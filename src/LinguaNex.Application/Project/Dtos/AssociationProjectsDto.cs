namespace LinguaNex.Project.Dtos
{
    public class AssociationProjectsDto
    {
        /// <summary>
        /// 可关联项目列表
        /// </summary>
        public List<ProjectDto> CanAssociationProjects { get; set; } = new();
        /// <summary>
        /// 已关联项目列表
        /// </summary>
        public List<ProjectDto> HasAssociationProjects { get; set; } = new();
    }
}
