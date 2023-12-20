using LinguaNex.Project.Dtos;
using Wheel.Core.Dto;
using Wheel.DependencyInjection;

namespace LinguaNex.Project
{
    public interface IProjectsAppService : ITransientDependency
    {
        Task<R<ProjectDto>> FindAsync(string id);
        Task<Page<ProjectDto>> PageListAsync(PageRequest request);
        Task<R<ProjectDto>> CreateAsync(CreateProjectDto dto);
        Task<R> DeleteAsync(string id);
        Task<R> UpdateEnableAsync(string id);
        Task<R> CreateProjectAssociation(CreateProjectAssociationDto dto);
        Task<R> DeleteProjectAssociation(DeleteProjectAssociationDto dto);
        Task<R<AssociationProjectsDto>> GetCanAssociationProjects(string projectId);
    }
}
