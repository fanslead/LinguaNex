﻿using LinguaNex.Resources;
using LinguaNex.Resources.Dtos;
using Microsoft.AspNetCore.Mvc;
using Wheel.Controllers;
using Wheel.Core.Dto;

namespace LinguaNex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController(IResourcesAppService resourcesAppService) : LinguaNexControllerBase
    {
        [HttpPost]
        public Task<R<ResourceDto>> CreateAsync([FromBody] CreateResourceDto dto)
        {
            return resourcesAppService.CreateAsync(dto);
        }
        [HttpDelete("{id}")]
        public Task<R> DeleteAsync(string id)
        {
            return resourcesAppService.DeleteAsync(id);
        }
        [HttpGet("all/{cultureId}")]
        public Task<R<List<ResourceDto>>> GetAllResourceByCulture(string cultureId)
        {
            return resourcesAppService.GetAllResourceByCulture(cultureId);
        }
        [HttpGet("list")]
        public Task<Page<ResourceDto>> GetResourcePageByCulture([FromQuery]ResourcePageRequest request)
        {
            return resourcesAppService.GetResourcePageByCulture(request);
        }
        [HttpPut()]
        public Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto)
        {
            return resourcesAppService.UpdateAsync(dto);
        }
    }
}
