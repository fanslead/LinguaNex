using LinguaNex.Dtos;
using Wheel.Core.Dto;
using Wheel.DependencyInjection;

namespace LinguaNex.OpenApi
{
    public interface IOpenApiAppService : ITransientDependency
    {
        Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool all);
    }
}
