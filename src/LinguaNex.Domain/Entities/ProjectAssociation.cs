namespace LinguaNex.Entities
{
    public class ProjectAssociation
    {
        public required virtual string MainProjectId { get; set; }
        public virtual Projects? MainProject { get; set; }
        public required virtual string AssociationProjectId { get; set; }
        public virtual Projects? AssociationProject { get; set; }
    }
}
