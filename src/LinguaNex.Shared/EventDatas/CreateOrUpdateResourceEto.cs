using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("CreateOrUpdateResourceEto")]
    public class CreateOrUpdateResourceEto
    {
        public long Id { get; set; }
    }
}
