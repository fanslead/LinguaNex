namespace LinguaNex.Resources.Dtos
{
    public class BatchUpdateResourceDto
    {
        public string Key { get; set; }
        public string ProjectId { get; set; }
        public List<BatchUpdateDto> Resouces { get; set; }
    }
    public class BatchUpdateDto
    {
        public string Value { get; set; }
        public long CultureId { get; set; }
    }
}
