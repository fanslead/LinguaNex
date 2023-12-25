using LinguaNex.Emuns;

namespace LinguaNex.Resources.Dtos
{
    public class CreateResourceDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string CultureId { get; set; }
        public string ProjectId { get; set; }

        public bool SyncCulture { get; set; } = false;
        public bool Translate { get; set; } = false;
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
