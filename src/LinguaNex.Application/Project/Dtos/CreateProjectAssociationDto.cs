namespace LinguaNex.Project.Dtos
{
    public class CreateProjectAssociationDto
    {
        public string MainProjectId { get; set; }
        public List<string> AssociationProjectIds { get; set; }
    }
}
