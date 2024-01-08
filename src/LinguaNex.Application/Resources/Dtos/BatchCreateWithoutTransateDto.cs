namespace LinguaNex.Resources.Dtos
{
    public class BatchCreateWithoutTransateDto
    {
        public string Key { get; set; }
        public string ProjectId { get; set; }
        public List<BatchCreateWithoutTransateResourceDto> Resouces { get; set; }
    }
    public class BatchCreateWithoutTransateResourceDto
    {
        public string Value { get; set; }
        public long CultureId { get; set; }
    }
}
