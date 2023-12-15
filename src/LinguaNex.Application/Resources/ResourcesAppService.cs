using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.Resources.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.Services;

namespace LinguaNex.Resources
{
    public class ResourcesAppService(IBasicRepository<Resource, string> resourceRepository) : LinguaNexServiceBase, IResourcesAppService
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
    }
}
