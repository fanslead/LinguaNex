using LinguaNex.Project;
using LinguaNex.Project.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Controllers;
using Wheel.Core.Dto;

namespace LinguaNex.Controllers
{
    /// <summary>
    /// 项目
    /// </summary>
    /// <param name="projectsAppService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectsAppService projectsAppService) : LinguaNexControllerBase
    {
        [HttpPost()]
        public Task<R<ProjectDto>> CreateAsync(CreateProjectDto dto)
        {
            return projectsAppService.CreateAsync(dto);
        }
        [HttpDelete("{id}")]
        public Task<R> DeleteAsync(string id)
        {
            return projectsAppService.DeleteAsync(id);
        }
        [HttpGet("{id}")]
        public Task<R<ProjectDto>> FindAsync(string id)
        {
            return projectsAppService.FindAsync(id);
        }
        [HttpGet()]
        public Task<Page<ProjectDto>> PageListAsync([FromQuery]PageRequest request)
        {
            return projectsAppService.PageListAsync(request);
        }
        [HttpPut("enable/{id}")]
        public Task<R> UpdateEnableAsync(string id)
        {
            return projectsAppService.UpdateEnableAsync(id);
        }
    }
}
