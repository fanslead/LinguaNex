using LinguaNex.Cultures.Dtos;
using LinguaNex.Entities;
using LinguaNex.Resources;
using LinguaNex.Resources.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Wheel.Controllers;
using Wheel.Core.Dto;

namespace LinguaNex.Controllers
{
    /// <summary>
    /// 多语言资源
    /// </summary>
    /// <param name="resourcesAppService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController(IResourcesAppService resourcesAppService) : LinguaNexControllerBase
    {
        /// <summary>
        /// Json文件批量导入
        /// </summary>
        /// <param name="cultureId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("File/{cultureId}")]
        public Task<R> BatchCreateByJsonFileAsync(long cultureId, [FromQuery]bool? translate, [FromForm]BatchCreateByJsonFileDto dto)
        {
            return resourcesAppService.BatchCreateByJsonFileAsync(cultureId, translate, dto);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<R<ResourceDto>> CreateAsync([FromBody] CreateResourceDto dto)
        {
            return resourcesAppService.CreateAsync(dto);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<R> DeleteAsync(long id)
        {
            return resourcesAppService.DeleteAsync(id);
        }
        /// <summary>
        /// 根据地区获取所有多语言资源
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        [HttpGet("all/{cultureId}")]
        public Task<R<List<ResourceDto>>> GetAllResourceByCulture(long cultureId)
        {
            return resourcesAppService.GetAllResourceByCulture(cultureId);
        }
        /// <summary>
        /// 根据项目获取所有多语言资源
        /// </summary>
        /// <param name="cultureId"></param>
        /// <returns></returns>
        [HttpGet("all/project/{projectId}")]
        public Task<R<CultureResourceAllInOneDto>> GetAllResourceByProject(string projectId)
        {
            return resourcesAppService.GetAllResourceByProject(projectId);
        }
        /// <summary>
        /// 根据地区分页获取多语言资源
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public Task<Page<ResourceDto>> GetResourcePageByCulture([FromQuery]ResourcePageRequest request)
        {
            return resourcesAppService.GetResourcePageByCulture(request); 
        }

        /// <summary>
        /// 根据项目分页获取多语言资源
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list/project")]
        public Task<Page<Dictionary<string, string>>> GetResourcePageByProject([FromQuery] ResourcePageRequest request)
        {
            return resourcesAppService.GetResourcePageByProject(request); 
        }
        /// <summary>
        /// 根据项目分页获取多语言资源表格列数据
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("list/columns/{projectId}")]
        public Task<R<List<AntdColumn>>> GetResourcePageByProjectTableColumns(string projectId)
        {
            return resourcesAppService.GetResourcePageByProjectTableColumns(projectId); 
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut()]
        public Task<R<ResourceDto>> UpdateAsync(UpdateResourceDto dto)
        {
            return resourcesAppService.UpdateAsync(dto);
        }
        /// <summary>
        /// 通过地区和Key匹配更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("ByCultureAndKey")]
        public Task<R<ResourceDto>> UpdateByCultureAndKeyAsync(UpdateResourceByCultureAndKeyDto dto)
        {
            return resourcesAppService.UpdateByCultureAndKeyAsync(dto);
        }
        /// <summary>
        /// 测试多语言
        /// </summary>
        /// <param name="testStr">测试字符串</param>
        /// <returns></returns>
        [HttpGet("Test")]
        public R<string> Test(string testStr)
        {
            return Success(L[testStr].ToString());
        }
    }
}
