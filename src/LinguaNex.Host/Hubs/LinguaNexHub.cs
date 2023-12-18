using LinguaNex.Dtos;
using LinguaNex.OpenApi;
using Microsoft.AspNetCore.SignalR;
using Wheel.Core.Dto;

namespace LinguaNex.Hubs
{
    public class LinguaNexHub(IOpenApiAppService openApiAppService) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if(Context.GetHttpContext().Request.Query.TryGetValue("project", out var project))
            {
                if(!string.IsNullOrWhiteSpace(project.ToString()))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, project.ToString());
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.GetHttpContext().Request.Query.TryGetValue("project", out var project))
            {
                if (!string.IsNullOrWhiteSpace(project.ToString()))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, project.ToString());
                }
            }
        }

        public Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool all)
        {
            return openApiAppService.GetResources(projectId, cultureName, all);
        }

    }
}
