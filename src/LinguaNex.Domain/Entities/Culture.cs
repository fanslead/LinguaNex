namespace LinguaNex.Entities
{
    public class Culture
    {
        public required long Id { get; set; }
        public required string Name { get; set; }
        public virtual List<Resource>? Resources { get; set; }
        public virtual required string ProjectId { get; set; }
        public virtual Projects? Project { get; set; }
    }
}
