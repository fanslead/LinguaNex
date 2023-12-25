using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Resources.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.Core.Exceptions;
using Wheel.Services;

namespace LinguaNex.Resources
{
    public class ResourcesAppService(IBasicRepository<Resource, string> resourceRepository, IBasicRepository<Culture, string> cultureRepository, IBasicRepository<Projects, string> projectsRepository) : LinguaNexServiceBase, IResourcesAppService
    {
        public async Task<R<List<ResourceDto>>> GetAllResourceByCulture(string cultureId)
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

        public async Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto)
        {
            if (!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.ProjectId);
            if (!await cultureRepository.AnyAsync(a => a.Id == dto.CultureId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.CultureId);
            if (await resourceRepository.AnyAsync(a => a.Id == dto.ProjectId && a.Id == dto.CultureId && a.Key == dto.Key))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.Exist).WithMessageDataData(dto.Key);

            var entity = Mapper.Map<Resource>(dto);
            entity.Id = SnowflakeIdGenerator.Create().ToString();
            entity = await resourceRepository.InsertAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            if(dto.SyncCulture || dto.Translate)
            {
                await DistributedEventBus.PublishAsync(new ResourceSyncCultureAndTranslateEto { Translate = dto.Translate, SyncCulture = dto.SyncCulture, Id = entity.Id, TranslateProvider = dto.TranslateProvider });
            }
            return Success(Mapper.Map<ResourceDto>(entity));
        }
        public async Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto)
        {
            var entity = await resourceRepository.FindAsync(dto.Id);
            if (entity == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.Id);

            entity.Value = dto.Value;
            entity = await resourceRepository.UpdateAsync(entity, true);
            await DistributedEventBus.PublishAsync(new CreateOrUpdateResourceEto { Id = entity.Id });
            return Success(Mapper.Map<ResourceDto>(entity));
        }

        public async Task<R> DeleteAsync(string id)
        {
            await resourceRepository.DeleteAsync(id, true);
            return Success();
        }
    }
}
