using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("BatchCreateResourceEto")]
    public class BatchCreateResourceEto
    {
        public string CultureId { get; set; }
        public string FirstResourceId { get; set; }
        public bool? Translate { get; set; }
    }
}
