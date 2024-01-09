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
    public class CreateOrUpdateResourceEventHandler(ILogger<CreateOrUpdateResourceEventHandler> logger, IBasicRepository<Resource, long> resourceRepository, IBasicRepository<ProjectAssociation> projectAssociationRepository, IHubContext<LinguaNexHub> hubContext) : IDistributedEventHandler<CreateOrUpdateResourceEto>, ITransientDependency
    {
        public async Task Handle(CreateOrUpdateResourceEto eventData, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"CreateOrUpdateResourceEventHandler Data: {eventData.ToJson()}");
            try
            {
                var resource = await resourceRepository.FindAsync(a => a.Id == eventData.Id, cancellationToken, a => a.Culture);
                if (resource != null)
                {
                    await hubContext.Clients.Group(resource.ProjectId.ToString()).SendAsync("CreateOrUpdateResource", new ResourcesDto
                    {
                        CultureName = resource?.Culture?.Name,
                        Resources = new Dictionary<string, string> { { resource?.Key, resource?.Value } }
                    }, cancellationToken);
                    var ps = await projectAssociationRepository.GetListAsync(a => a.AssociationProjectId == resource.ProjectId);
                    foreach (var p in ps)
                    {
                        if (!await resourceRepository.AnyAsync(a => a.Key == resource.Key && a.ProjectId == p.MainProjectId))
                        {
                            await hubContext.Clients.Group(p.MainProjectId.ToString()).SendAsync("CreateOrUpdateResource", new ResourcesDto
                            {
                                CultureName = resource?.Culture?.Name,
                                Resources = new Dictionary<string, string> { { resource?.Key, resource?.Value } }
                            }, cancellationToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ex.ReThrow();
            }
        }
    }
}
