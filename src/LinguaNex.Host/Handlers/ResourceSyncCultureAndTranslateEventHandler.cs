using LinguaNex.EventDatas;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;

namespace LinguaNex.Handlers
{
    public class ResourceSyncCultureAndTranslateEventHandler : IDistributedEventHandler<ResourceSyncCultureAndTranslateEto>, ITransientDependency
    {
        public Task Handle(ResourceSyncCultureAndTranslateEto eventData, CancellationToken cancellationToken = default)
        {
            // TODO: 
            return Task.CompletedTask;
        }
    }
}
