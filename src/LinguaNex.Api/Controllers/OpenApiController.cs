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
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="cultureName">地区语言码</param>
        /// <param name="all">是否获取所有</param>
        /// <returns></returns>
        [HttpGet("Resources/{projectId}")]
        public Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool all)
        {
            return openApiAppService.GetResources(projectId, cultureName, all);
        }
    }
}
