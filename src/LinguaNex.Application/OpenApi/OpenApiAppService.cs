using LinguaNex.Domain;
using LinguaNex.Dtos;
using LinguaNex.Entities;
using System.Globalization;
using Wheel.Core.Dto;
using Wheel.Services;

namespace LinguaNex.OpenApi
{
    public class OpenApiAppService(IBasicRepository<Culture, string> cultureRepository) : LinguaNexServiceBase, IOpenApiAppService
    {
        public async Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool? all)
        {
            var datas = await cultureRepository.GetListAsync(
                cultureRepository.BuildPredicate(
                    (true, a => a.ProjectId == projectId),
                    (!string.IsNullOrWhiteSpace(cultureName), a => a.Name == cultureName),
                    (!all.HasValue && string.IsNullOrWhiteSpace(cultureName), a => a.Name == CultureInfo.CurrentUICulture.Name)
                    ),
                propertySelectors: a => a.Resources
                );
            return Success(datas.Select(a => new ResourcesDto
            {
                CultureName = a.Name,
                Resources = a.Resources.ToDictionary(b => b.Key, b => b.Value)
            }).ToList());
        }
    }
}
