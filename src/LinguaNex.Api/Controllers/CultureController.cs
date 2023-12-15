using LinguaNex.Cultures;
using LinguaNex.Cultures.Dtos;
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
        public Task<R> DeleteAsync(string id)
        {
            return cultureAppService.DeleteAsync(id);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet()]
        public Task<Page<CultureDto>> PageListAsync([FromQuery]PageRequest request)
        {
            return cultureAppService.PageListAsync(request);
        }
    }
}
