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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost()]
        public Task<R<ProjectDto>> CreateAsync(CreateProjectDto dto)
        {
            return projectsAppService.CreateAsync(dto);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<R> DeleteAsync(string id)
        {
            return projectsAppService.DeleteAsync(id);
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<R<ProjectDto>> FindAsync(string id)
        {
            return projectsAppService.FindAsync(id);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        public Task<Page<ProjectDto>> PageListAsync([FromQuery]PageRequest request)
        {
            return projectsAppService.PageListAsync(request);
        }
        /// <summary>
        /// 修改是否启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("enable/{id}")]
        public Task<R> UpdateEnableAsync(string id)
        {
            return projectsAppService.UpdateEnableAsync(id);
        }
    }
}
