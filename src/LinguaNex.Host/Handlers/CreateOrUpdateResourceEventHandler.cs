using LinguaNex.Domain;
using LinguaNex.Dtos;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Hubs;
using Microsoft.AspNetCore.SignalR;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;

namespace LinguaNex.Handlers
{
    public class CreateOrUpdateResourceEventHandler(IBasicRepository<Resource, string> resourceRepository, IBasicRepository<ProjectAssociation> projectAssociationRepository, IHubContext<LinguaNexHub> hubContext) : IDistributedEventHandler<CreateOrUpdateResourceEto>, ITransientDependency
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
                var ps = await projectAssociationRepository.GetListAsync(a => a.AssociationProjectId == resource.ProjectId);
                foreach( var p in ps)
                {
                    if(!await resourceRepository.AnyAsync(a=>a.Key == resource.Key && a.ProjectId == p.MainProjectId))
                    {
                        await hubContext.Clients.Group(p.MainProjectId).SendAsync("CreateOrUpdateResource", new ResourcesDto
                        {
                            CultureName = resource?.Culture?.Name,
                            Resources = new Dictionary<string, string> { { resource?.Key, resource?.Value } }
                        }, cancellationToken);
                    }
                }
            }
        }
    }
}
