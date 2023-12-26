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
        Task<R<List<ResourceDto>>> GetAllResourceByCulture(string cultureId);

        Task<Page<ResourceDto>> GetResourcePageByCulture(ResourcePageRequest request);
        Task<R> BatchCreateByJsonFileAsync(string cultureId, BatchCreateByJsonFileDto dto);
        Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto);

        Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto);

        Task<R> DeleteAsync(string id);
    }
}
