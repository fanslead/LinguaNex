using LinguaNex.Emuns;
using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("BatchCreateResourceEto")]
    public class BatchCreateResourceEto
    {
        public long CultureId { get; set; }
        public long FirstResourceId { get; set; }
        public bool? Translate { get; set; }
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
