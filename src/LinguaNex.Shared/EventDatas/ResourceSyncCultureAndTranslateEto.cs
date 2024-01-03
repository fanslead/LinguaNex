using LinguaNex.Emuns;
using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("ResourceSyncCultureAndTranslateEto")]
    public class ResourceSyncCultureAndTranslateEto
    {
        public long Id { get; set; }
        public bool SyncCulture { get; set; } 
        public bool Translate { get; set; }
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
