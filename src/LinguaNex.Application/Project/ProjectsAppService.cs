using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.Project.Dtos;
using Wheel.Core.Dto;
using Wheel.Services;

namespace LinguaNex.Project
{
    public class ProjectsAppService(IBasicRepository<Projects, string> projectsRepository, IBasicRepository<ProjectAssociation> projectsAssociationRepository) : LinguaNexServiceBase, IProjectsAppService
    {
        public async Task<R<ProjectDto>> FindAsync(string id)
        {
            var entity = await projectsRepository.FindAsync(id);
            return Success(Mapper.Map<ProjectDto>(entity));
        }
        public async Task<Page<ProjectDto>> PageListAsync(PageRequest request)
        {   
            var (entities, total) = await projectsRepository.GetPageListAsync(a => true, (request.PageIndex - 1) * request.PageSize, request.PageSize, request.OrderBy);
            return Page(Mapper.Map<List<ProjectDto>>(entities), total);
        }

        public async Task<R<ProjectDto>> CreateAsync(CreateProjectDto dto)
        {
            var entity = Mapper.Map<Projects>(dto);
            entity.Id = GuidGenerator.Create().ToString();
            entity = await projectsRepository.InsertAsync(entity, true);
            return Success(Mapper.Map<ProjectDto>(entity));
        }

        public async Task<R> DeleteAsync(string id)
        {
            await projectsRepository.DeleteAsync(id, true);
            return Success();
        }

        public async Task<R> UpdateEnableAsync(string id)
        {
            await projectsRepository.UpdateAsync(a=>a.Id == id, a=> a.SetProperty(e=>e.Enalbe, e => !e.Enalbe), true);
            return Success();
        }

        public async Task<R> CreateProjectAssociation(CreateProjectAssociationDto dto)
        {
            await projectsAssociationRepository.InsertManyAsync(dto.AssociationProjectIds.Select(a => new ProjectAssociation { MainProjectId = dto.MainProjectId, AssociationProjectId = a }).ToList(), true);
            return Success();
        }

        public async Task<R> DeleteProjectAssociation(DeleteProjectAssociationDto dto)
        {
            await projectsAssociationRepository.DeleteAsync(a => a.MainProjectId == dto.MainProjectId && a.AssociationProjectId == dto.AssociationProjectId, true);
            return Success();
        }

        public async Task<R<List<ProjectDto>>> GetCanAssociationProjects(string projectId)
        {
            var projectAssociations = await projectsAssociationRepository.SelectListAsync(a => a.MainProjectId == projectId, a=>a.AssociationProjectId);
            var entities = await projectsRepository.GetListAsync(a => a.Id != projectId && projectAssociations.Contains(a.Id));

            return Success(Mapper.Map<List<ProjectDto>>(entities));
        }
    }
}
