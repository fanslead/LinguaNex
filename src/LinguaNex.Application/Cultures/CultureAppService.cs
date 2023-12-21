using LinguaNex.Const;
using LinguaNex.Cultures.Dtos;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.Project.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.Core.Exceptions;
using Wheel.Services;

namespace LinguaNex.Cultures
{
    public class CultureAppService(IBasicRepository<Culture, string> cultureRepository, IBasicRepository<Projects, string> projectsRepository) : LinguaNexServiceBase, ICultureAppService
    {
        public async Task<R<CultureDto>> CreateAsync(CreateCultureDto dto)
        {
            if (!SupportedCulture.All().Any(a => a.Name == dto.Name))
                throw new BusinessException(ErrorCode.NotSupported, ErrorCode.NotSupported).WithMessageDataData(dto.Name);

            if(!await projectsRepository.AnyAsync(a => a.Id == dto.ProjectId))
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(dto.ProjectId);

            if(await cultureRepository.AnyAsync(a => a.Name == dto.Name && a.ProjectId == dto.ProjectId))
                throw new BusinessException(ErrorCode.Exist, ErrorCode.Exist).WithMessageDataData(dto.Name);

            var entity = Mapper.Map<Culture>(dto);
            entity.Id = SnowflakeIdGenerator.Create().ToString();
            entity = await cultureRepository.InsertAsync(entity, true);
            return Success(Mapper.Map<CultureDto>(entity));
        }

        public async Task<Page<CultureDto>> PageListAsync(CulturePageRequest request)
        {
            var (entities, total) = await cultureRepository.GetPageListAsync(a => a.ProjectId == request.ProjectId, (request.PageIndex - 1) * request.PageSize, request.PageSize, request.OrderBy);
            return Page(Mapper.Map<List<CultureDto>>(entities), total);
        }

        public async Task<R> DeleteAsync(string id)
        {
            await cultureRepository.DeleteAsync(a => a.Id == id, true);
            return Success();
        }
    }
}
