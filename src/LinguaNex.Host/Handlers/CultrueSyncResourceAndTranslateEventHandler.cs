using LinguaNex.EventDatas;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;

namespace LinguaNex.Handlers
{
    public class CultrueSyncResourceAndTranslateEventHandler : IDistributedEventHandler<CultrueSyncResourceAndTranslateEto>, ITransientDependency
    {
        public Task Handle(CultrueSyncResourceAndTranslateEto eventData, CancellationToken cancellationToken = default)
        {
            //TODO:
            return Task.CompletedTask;
        }
    }
}
