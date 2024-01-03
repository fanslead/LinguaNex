using LinguaNex.Emuns;
using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("CultrueSyncResourceAndTranslateEto")]
    public class CultrueSyncResourceAndTranslateEto
    {
        public long Id { get; set; }
        public bool SyncResource { get; set; } = false;
        public bool Translate { get; set; } = false;
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
