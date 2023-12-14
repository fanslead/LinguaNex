using LinguaNex.Dtos;
using LinguaNex.OpenApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Wheel.Core.Dto;

namespace LinguaNex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenApiController(IOpenApiAppService openApiAppService) : ControllerBase
    {
        [HttpGet("Resources/{projectId}")]
        public Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool? all)
        {
            return openApiAppService.GetResources(projectId, cultureName, all);
        }
    }
}
