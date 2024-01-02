using LinguaNex.EventDatas;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;

namespace LinguaNex.Handlers
{
    public class BatchCreateResourceEventHandler : IDistributedEventHandler<BatchCreateResourceEto>, ITransientDependency
    {
        public Task Handle(BatchCreateResourceEto eventData, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
