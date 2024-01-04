using LinguaNex.Resources.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.DependencyInjection;

namespace LinguaNex.Resources
{
    public interface IResourcesAppService : ITransientDependency
    {
        Task<R<List<ResourceDto>>> GetAllResourceByCulture(long cultureId);
        Task<R<List<Dictionary<string, string>>>> GetAllResourceByProject(string projectId);
        Task<Page<ResourceDto>> GetResourcePageByCulture(ResourcePageRequest request);
        Task<R> BatchCreateByJsonFileAsync(long cultureId, bool? translate, BatchCreateByJsonFileDto dto);
        Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto);

        Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto);
        Task<R<ResourceDto>> UpdateByCultureAndKeyAsync(UpdateResourceByCultureAndKeyDto dto);

        Task<R> DeleteAsync(long id);
    }
}
