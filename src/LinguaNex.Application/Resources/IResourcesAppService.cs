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
        Task<R<CultureResourceAllInOneDto>> GetAllResourceByProject(string projectId);
        Task<Page<ResourceDto>> GetResourcePageByCulture(ResourcePageRequest request);
        Task<Page<Dictionary<string, string>>> GetResourcePageByProject(ResourcePageRequest request);
        Task<R<List<AntdColumn>>> GetResourcePageByProjectTableColumns(string projectId);
        Task<R> BatchCreateByJsonFileAsync(long cultureId, bool? translate, BatchCreateByJsonFileDto dto);
        Task<R<ResourceDto>> CreateAsync(CreateResourceDto dto);

        Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto);
        Task<R<ResourceDto>> UpdateByCultureAndKeyAsync(UpdateResourceByCultureAndKeyDto dto);

        Task<R> DeleteAsync(long id);
        Task<R<Dictionary<string, string>>> TransateMultipleLanguages(TransateMultipleLanguagesDto dto);
        Task<R> BatchCreateWithoutTransate(BatchCreateWithoutTransateDto dto);
        Task<R> BatchUpdate(BatchUpdateResourceDto dto);
    }
}
