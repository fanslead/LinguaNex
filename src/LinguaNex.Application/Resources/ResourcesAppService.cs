﻿using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Resources.Dtos;
using LinguaNex.Translates;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Wheel.Core.Dto;
using Wheel.Core.Exceptions;
using Wheel.Services;
using System.Linq.Dynamic.Core;
using LinguaNex.Emuns;

namespace LinguaNex.Resources
{
    public class ResourcesAppService(IBasicRepository<Resource, long> resourceRepository, IBasicRepository<Culture, long> cultureRepository, IBasicRepository<Projects, long> projectsRepository, ITranslateAppService translateAppService) : LinguaNexServiceBase, IResourcesAppService
    {
        public async Task<R<List<ResourceDto>>> GetAllResourceByCulture(long cultureId)
        {
            var datas = await resourceRepository.SelectListAsync(a => a.CultureId == cultureId,
                a => new ResourceDto
                {
                    Id = a.Id,
                    CultureId = a.CultureId,
                    ProjectId = a.ProjectId,
                    Key = a.Key,
                    Value = a.Value
                });
            return Success(datas);
        }

        public async Task<R<CultureResourceAllInOneDto>> GetAllResourceByProject(string projectId)
        {
            var datas = await resourceRepository.GetListAsync(a => a.ProjectId == projectId, propertySelectors: a => a.Culture);
            var goupData = datas.GroupBy(a => a.Key);
            var dicData = goupData.Select(a =>
            {
                var dic = a.ToDictionary(a => a.Culture.Name, a => a.Value);
                dic.Add("key", a.Key);
                return dic;
            }).ToList();
            var columns = new List<AntdColumn>
            {
                new AntdColumn { DataIndex = "key", Title = "Key", ShortTitle = "Key" }
            };
            columns.AddRange(datas.GroupBy(a => a.Culture.Name).Select(a => new AntdColumn { DataIndex = a.Key, Title = SupportedCulture.ChineseLanguages[a.Key], ShortTitle = SupportedCulture.EnglishLanguages[a.Key] }));
            return Success(new CultureResourceAllInOneDto { Columns = columns, Resources = dicData });
        }

        public async Task<Page<ResourceDto>> GetResourcePageByCulture(ResourcePageRequest request)
        {
            var (datas, total) = await resourceRepository.SelectPageListAsync(a => a.CultureId == request.CultureId,
                a => new ResourceDto
                {
                    Id = a.Id,
                    CultureId = a.CultureId,
                    ProjectId = a.ProjectId,
                    Key = a.Key,
                    Value = a.Value
                },
                (request.PageIndex - 1) * request.PageSize,
                request.PageSize
                );
            return Page(datas, total);
        }

        public async Task<Page<Dictionary<string, CultureResourceDto>>> GetResourcePageByProject(ResourcePageRequest request)
        {
            var query = resourceRepository.GetQueryableWithIncludes(a => a.Culture).OrderBy(request.OrderBy).Where(a => a.ProjectId == request.ProjectId);
            if (!request.Key.IsNullOrWhiteSpace())
            {
                query = query.Where(a => request.Key.Contains(a.Key));
            }
            var groupQuery = query.GroupBy(a => a.Key);
            var total = await groupQuery.CountAsync();
            var datas = (await groupQuery.ToListAsync()).Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(a =>
            {
                var dic = a.ToDictionary(a => a.Culture.Name, a => new CultureResourceDto { Value = a.Value, CultureId = a.CultureId });
                dic.Add("key", new CultureResourceDto { Value = a.Key });
                return dic;
            }).ToList();
            return Page(datas, total);
        }
        public async Task<R<List<AntdColumn>>> GetResourcePageByProjectTableColumns(string projectId)
        {
            var cultures = await cultureRepository.GetListAsync(a => a.ProjectId == projectId);

            var columns = new List<AntdColumn>
            {
                new AntdColumn { DataIndex = "key", Title = "Key", ShortTitle = "Key" }
            };
            columns.AddRange(cultures.Select(a => new AntdColumn { DataIndex = a.Name, Title = SupportedCulture.ChineseLanguages[a.Name], ShortTitle = SupportedCulture.EnglishLanguages[a.Name] }));

            return Success(columns);
        }

        public async Task<R> BatchCreateByJsonFileAsync(long cultureId, bool? translate, TranslateProviderEnum? translateProvider, BatchCreateByJsonFileDto dto)
        {
            if (Path.GetExtension(dto.File.FileName) != ".json")
                throw new BusinessException(ErrorCode.NotSupported, ErrorCode.NotSupported).WithMessageData(Path.GetExtension(dto.File.FileName));

            var culture = await cultureRepository.FindAsync(cultureId);
            if (culture == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(cultureId.ToString());

            using (var stream = dto.File.OpenReadStream())
            {
                var doc = await JsonDocument.ParseAsync(stream);
                var resources = TraverseJson(doc.RootElement, "", culture.Id, culture.ProjectId);
                if (resources.Count() > 0)
                {
                    var existKeys = resources.Select(a => a.Key).ToList();
                    await resourceRepository.DeleteAsync(a => existKeys.Contains(a.Key), true);
                    await resourceRepository.InsertManyAsync(resources, true);
                    await DistributedEventBus.PublishAsync(new BatchCreateResourceEto { CultureId = cultureId, FirstResourceId = resources.First().Id, Translate = translate, TranslateProvider = translateProvider });
                }
            }
            return Success();
        }
        public async Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto)
        {
            if (!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.ProjectId.ToString());
            if (!await cultureRepository.AnyAsync(a => a.Id == dto.CultureId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.CultureId.ToString());
            if (await resourceRepository.AnyAsync(a => a.ProjectId == dto.ProjectId && a.CultureId == dto.CultureId && a.Key == dto.Key))
                throw new BusinessException(ErrorCode.Exist, ErrorCode.Exist).WithMessageData(dto.Key);

            var entity = Mapper.Map<Resource>(dto);
            entity.Id = SnowflakeIdGenerator.Create();
            entity = await resourceRepository.InsertAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            if (dto.SyncCulture.Value)
            {
                await DistributedEventBus.PublishAsync(new ResourceSyncCultureAndTranslateEto { Translate = dto.Translate.Value, SyncCulture = dto.SyncCulture.Value, Id = entity.Id, TranslateProvider = dto.TranslateProvider });
            }
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto)
        {
            var entity = await resourceRepository.FindAsync(dto.Id);
            if (entity == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.Id.ToString());

            entity.Value = dto.Value;
            entity = await resourceRepository.UpdateAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R<ResourceDto>> UpdateByCultureAndKeyAsync(UpdateResourceByCultureAndKeyDto dto)
        {
            var entity = await resourceRepository.FindAsync(a => a.Key == dto.Key && a.Culture.Name == dto.Culture);
            if (entity == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.Key.ToString());

            entity.Value = dto.Value;
            entity = await resourceRepository.UpdateAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R> BatchCreateWithoutTransate(BatchCreateWithoutTransateDto dto)
        {
            if (!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.ProjectId.ToString());

            var entities = dto.Resouces.Select(a => new Resource
            {
                Id = SnowflakeIdGenerator.Create(),
                Key = dto.Key,
                Value = a.Value,
                ProjectId = dto.ProjectId,
                CultureId = a.CultureId,
            }).ToList();
            await resourceRepository.InsertManyAsync(entities, true);
            return Success();
        }
        public async Task<R> BatchUpdate(BatchUpdateResourceDto dto)
        {
            if (!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(dto.ProjectId.ToString());
            var cultureIds = dto.Resouces.Select(a => a.CultureId).ToList();
            var entities = await resourceRepository.GetListAsync(a => a.ProjectId == dto.ProjectId && cultureIds.Contains(a.CultureId) && a.Key == dto.Key);
            var updateDic = dto.Resouces.ToDictionary(a => a.CultureId, a => a.Value);
            foreach (var entity in entities)
            {
                entity.Value = updateDic[entity.CultureId];
            }
            await resourceRepository.UpdateManyAsync(entities, true);
            return Success();
        }
        public async Task<R<Dictionary<string, string>>> TransateMultipleLanguages(TransateMultipleLanguagesDto dto)
        {
            var langDic = new Dictionary<string, string>();
            langDic.TryAdd(dto.SourceLanguage, dto.Value);
            var cultures = await cultureRepository.GetListAsync(a => a.ProjectId == dto.ProjectId && a.Name != dto.SourceLanguage);
            foreach (var culture in cultures)
            {
                var translateResult = await translateAppService.Translate(new Translates.Dto.TranslateRequestDto
                {
                    SourceLang = dto.SourceLanguage,
                    SourceString = dto.Value,
                    TargetLang = culture.Name,
                    TranslateProvider = dto.TranslateProvider
                });
                langDic.TryAdd(culture.Name, translateResult);
            }
            return Success(langDic);
        }
        public async Task<R> DeleteAsync(long id)
        {
            var resource = await resourceRepository.FindAsync(id);
            await resourceRepository.DeleteAsync(a => a.Key == resource.Key && a.ProjectId == resource.ProjectId, true);
            return Success();
        }
        public async Task<R> DeleteByKeyAsync(string key, string projectId)
        {
            await resourceRepository.DeleteAsync(a => a.Key == key && a.ProjectId == projectId, true);
            return Success();
        }
        private List<Resource> TraverseJson(JsonElement element, string parent, long cultureId, string projectId)
        {
            List<Resource> resources = new();
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        if (property.Value.ValueKind == JsonValueKind.Object)
                        {
                            resources.AddRange(TraverseJson(property.Value, $"{parent}{property.Name}.", cultureId, projectId));
                        }
                        else
                        {
                            resources.Add(new Resource
                            {
                                Id = SnowflakeIdGenerator.Create(),
                                ProjectId = projectId,
                                CultureId = cultureId,
                                Key = $"{parent}{property.Name}",
                                Value = property.Value.GetString()
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
            return resources;
        }
    }
}
