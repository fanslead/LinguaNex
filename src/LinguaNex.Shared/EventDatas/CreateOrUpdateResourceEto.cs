using Wheel.EventBus;

namespace LinguaNex.EventDatas
{
    [EventName("CreateOrUpdateResourceEto")]
    public class CreateOrUpdateResourceEto
    {
        public string Id { get; set; }
    }
}
