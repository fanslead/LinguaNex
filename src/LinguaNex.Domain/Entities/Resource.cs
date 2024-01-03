namespace LinguaNex.Entities
{
    public class Resource
    {
        public required long Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public virtual required long CultureId { get; set; }
        public virtual Culture? Culture { get; set; }
        public virtual required string ProjectId { get; set; }
        public virtual Projects? Project { get; set; }
    }
}
