using LinguaNex.Domain;
using LinguaNex.Dtos;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Hubs;
using Microsoft.AspNetCore.SignalR;
using Wheel.EventBus.Distributed;

namespace LinguaNex.Handlers
{
    public class CreateOrUpdateResourceEventHandler(IBasicRepository<Resource, string> resourceRepository, IHubContext<LinguaNexHub> hubContext) : IDistributedEventHandler<CreateOrUpdateResourceEto>
    {
        public async Task Handle(CreateOrUpdateResourceEto eventData, CancellationToken cancellationToken = default)
        {
            var resource = await resourceRepository.FindAsync(a => a.Id == eventData.Id, cancellationToken, a => a.Culture);
            if(resource != null)
            {
                await hubContext.Clients.Group(resource.ProjectId).SendAsync("CreateOrUpdateResource", new ResourcesDto
                {
                    CultureName = resource?.Culture?.Name,
                    Resources = new Dictionary<string, string> { { resource?.Key, resource?.Value } }
                }, cancellationToken);
            }
        }
    }
}
