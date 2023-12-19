using LinguaNex.Dtos;
using LinguaNex.OpenApi;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Reflection;
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
        public async Task<List<ResourcesDto>> GetResources(string projectId, string? cultureName, bool all)
        {
            return (await openApiAppService.GetResources(projectId, cultureName, all)).Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet("Resources/Json/{projectId}")]
        public async Task<IActionResult> ExportJson(string projectId, string? cultureName)
        {
            var result = await openApiAppService.GetResources(projectId, cultureName, string.IsNullOrWhiteSpace(cultureName));
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in result.Data)
                    {
                        var resourceBytes = resource.ToJson().GetBytes();

                        ZipArchiveEntry entry = zip.CreateEntry($"{resource.CultureName}.json");
                        using (Stream sw = entry.Open())
                        {
                            sw.Write(resourceBytes, 0, resourceBytes.Length);
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "json.zip");
            }

            void InvokeWriteFile(ZipArchive zipArchive)
            {
                foreach (MethodInfo method in zipArchive.GetType().GetRuntimeMethods())
                {
                    if (method.Name == "WriteFile")
                    {
                        method.Invoke(zipArchive, new object[0]);
                    }
                }
            }
        }
    }
}
