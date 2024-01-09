using LinguaNex.Const;
using LinguaNex.Cultures;
using LinguaNex.Cultures.Dtos;
using Microsoft.AspNetCore.Mvc;
using Wheel.Controllers;
using Wheel.Core.Dto;

namespace LinguaNex.Controllers
{
    /// <summary>
    /// 地区
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CultureController(ICultureAppService cultureAppService) : LinguaNexControllerBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost()]
        public Task<R<CultureDto>> CreateAsync(CreateCultureDto dto)
        {
            return cultureAppService.CreateAsync(dto);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task<R> DeleteAsync(long id)
        {
            return cultureAppService.DeleteAsync(id);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        public Task<Page<CultureDto>> PageListAsync([FromQuery] CulturePageRequest request)
        {
            return cultureAppService.PageListAsync(request);
        }
        /// <summary>
        /// 获取支持的地区码
        /// </summary>
        /// <returns></returns>
        [HttpGet("SupportedCultures")]
        public R<List<SupportedCulture>> GetNeutralCultures()
        {
            return Success(SupportedCulture.All());
        }
    }
}
