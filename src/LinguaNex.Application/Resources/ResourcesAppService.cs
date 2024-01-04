using IdGen;
using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Resources.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.Core.Exceptions;
using Wheel.Services;

namespace LinguaNex.Resources
{
    public class ResourcesAppService(IBasicRepository<Resource, long> resourceRepository, IBasicRepository<Culture, long> cultureRepository, IBasicRepository<Projects, long> projectsRepository) : LinguaNexServiceBase, IResourcesAppService
    {
        public async Task<R<List<ResourceDto>>> GetAllResourceByCulture(long cultureId)
        {
            var datas = await resourceRepository.SelectListAsync(a => a.CultureId == cultureId, 
                a=> new ResourceDto 
                {
                    Id = a.Id,
                    CultureId = a.CultureId,
                    ProjectId = a.ProjectId,
                    Key = a.Key,
                    Value =a.Value
                });
            return Success(datas);
        }

        public async Task<R<List<CultureResourceAllInOneDto>>> GetAllResourceByProject(string projectId)
        {
            var datas = await resourceRepository.GetListAsync(a => a.ProjectId == projectId, propertySelectors: a=>a.Culture);
            return Success(datas.GroupBy(a=>a.Key).Select(a=> new CultureResourceAllInOneDto { Key = a.Key, Resources = a.ToDictionary(a=>a.Culture.Name, a=>a.Value)}).ToList());
        }

        public async Task<Page<ResourceDto>> GetResourcePageByCulture(ResourcePageRequest request)
        {
            var (datas, total)= await resourceRepository.SelectPageListAsync(a => a.CultureId == request.CultureId, 
                a=> new ResourceDto 
                {
                    Id = a.Id,
                    CultureId = a.CultureId,
                    ProjectId = a.ProjectId,
                    Key = a.Key,
                    Value =a.Value
                },
                (request.PageIndex -1) * request.PageSize,
                request.PageSize
                );
            return Page(datas, total);
        }
        public async Task<R> BatchCreateByJsonFileAsync(long cultureId, bool? translate, BatchCreateByJsonFileDto dto)
        {
            if(Path.GetExtension(dto.File.FileName)!=".json")
                throw new BusinessException(ErrorCode.NotSupported, ErrorCode.NotSupported).WithMessageDataData(Path.GetExtension(dto.File.FileName));

            var culture = await cultureRepository.FindAsync(cultureId);
            if(culture == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(cultureId.ToString());
            
            using(var stream = dto.File.OpenReadStream())
            {
                var doc = await JsonDocument.ParseAsync(stream);
                var resources = TraverseJson(doc.RootElement, "", culture.Id, culture.ProjectId);
                if(resources.Count() > 0)
                {
                    var existKeys = resources.Select(a => a.Key).ToList();
                    await resourceRepository.DeleteAsync(a => existKeys.Contains(a.Key), true);
                    await resourceRepository.InsertManyAsync(resources, true);
                    await DistributedEventBus.PublishAsync(new BatchCreateResourceEto { CultureId = cultureId, FirstResourceId = resources.First().Id, Translate = translate });
                }
            }
            return Success();
        }
        public async Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto)
        {
            if (!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.ProjectId.ToString());
            if (!await cultureRepository.AnyAsync(a => a.Id == dto.CultureId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.CultureId.ToString());
            if (await resourceRepository.AnyAsync(a => a.ProjectId == dto.ProjectId && a.CultureId == dto.CultureId && a.Key == dto.Key))
                throw new BusinessException(ErrorCode.Exist, ErrorCode.Exist).WithMessageDataData(dto.Key);

            var entity = Mapper.Map<Resource>(dto);
            entity.Id = SnowflakeIdGenerator.Create();
            entity = await resourceRepository.InsertAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            if(dto.SyncCulture.Value)
            {
                await DistributedEventBus.PublishAsync(new ResourceSyncCultureAndTranslateEto { Translate = dto.Translate.Value, SyncCulture = dto.SyncCulture.Value, Id = entity.Id, TranslateProvider = dto.TranslateProvider });
            }
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto)
        {
            var entity = await resourceRepository.FindAsync(dto.Id);
            if (entity == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.Id.ToString());

            entity.Value = dto.Value;
            entity = await resourceRepository.UpdateAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R<ResourceDto>> UpdateByCultureAndKeyAsync(UpdateResourceByCultureAndKeyDto dto)
        {
            var entity = await resourceRepository.FindAsync(a=>a.Key == dto.Key && a.Culture.Name == dto.Culture);
            if (entity == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.Key.ToString());

            entity.Value = dto.Value;
            entity = await resourceRepository.UpdateAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            return Success(Mapper.Map<ResourceDto>(entity));
        }

        public async Task<R> DeleteAsync(long id)
        {
            var resource = await resourceRepository.FindAsync(id);
            await resourceRepository.DeleteAsync(a=>a.Key == resource.Key && a.ProjectId == resource.ProjectId, true);
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
