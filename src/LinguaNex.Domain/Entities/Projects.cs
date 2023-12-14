namespace LinguaNex.Entities
{
    public class Projects
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public bool Enalbe { get; set; }

        public virtual List<ProjectAssociation>? ProjectAssociations { get; set; }
    }
}
