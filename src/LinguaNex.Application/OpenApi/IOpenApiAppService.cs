using LinguaNex.Dtos;
using Wheel.Core.Dto;

namespace LinguaNex.OpenApi
{
    public interface IOpenApiAppService
    {
        Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool? all);
    }
}
