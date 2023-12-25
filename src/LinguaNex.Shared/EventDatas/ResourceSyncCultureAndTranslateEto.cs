using LinguaNex.Emuns;

namespace LinguaNex.EventDatas
{
    public class ResourceSyncCultureAndTranslateEto
    {
        public string Id { get; set; }
        public bool SyncCulture { get; set; } 
        public bool Translate { get; set; }
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
