using LinguaNex.Const;
using LinguaNex.Dtos;
using LinguaNex.OpenApi;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Wheel.Controllers;
using Wheel.Core.Dto;
namespace LinguaNex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenApiController(IOpenApiAppService openApiAppService) : LinguaNexControllerBase
    {
        /// <summary>
        /// 获取支持的地区码
        /// </summary>
        /// <returns></returns>
        [HttpGet("SupportedCultures")]
        public R<List<SupportedCulture>> GetNeutralCultures()
        {
            return Success(SupportedCulture.All());
        }
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
        /// 导出JSON文件
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
                        var resourceBytes = resource.ToJson(new System.Text.Json.JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                        }).GetBytes();

                        ZipArchiveEntry entry = zip.CreateEntry($"{resource.CultureName}.json");
                        using (Stream sw = entry.Open())
                        {
                            await sw.WriteAsync(resourceBytes, 0, resourceBytes.Length);
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    await ms.ReadAsync(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "json.zip");
            }
        }
        /// <summary>
        /// 导出toml文件
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet("Resources/toml/{projectId}")]
        public async Task<IActionResult> ExportToml(string projectId, string? cultureName)
        {
            var result = await openApiAppService.GetResources(projectId, cultureName, string.IsNullOrWhiteSpace(cultureName));
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in result.Data)
                    {
                        using(var resourceStream = new MemoryStream())
                        {
                            var sw = new StreamWriter(resourceStream);
                            foreach (var r in resource.Resources)
                            {
                                await sw.WriteLineAsync($"{r.Key}={r.Value}");
                            }
                            await sw.FlushAsync();
                            ZipArchiveEntry entry = zip.CreateEntry($"{resource.CultureName}.toml");
                            using(var writer = entry.Open())
                            {
                                var bt = resourceStream.ToArray();
                                await writer.WriteAsync(bt, 0, bt.Length);
                            }
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    await ms.ReadAsync(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "toml.zip");
            }
        }
        /// <summary>
        /// 导出properties文件
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet("Resources/Properties/{projectId}")]
        public async Task<IActionResult> ExportProperties(string projectId, string? cultureName)
        {
            var result = await openApiAppService.GetResources(projectId, cultureName, string.IsNullOrWhiteSpace(cultureName));
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in result.Data)
                    {
                        using(var resourceStream = new MemoryStream())
                        {
                            var sw = new StreamWriter(resourceStream);
                            foreach (var r in resource.Resources)
                            {
                                await sw.WriteLineAsync($"{r.Key}={r.Value}");
                            }
                            await sw.FlushAsync();
                            ZipArchiveEntry entry = zip.CreateEntry($"messages_{resource.CultureName.Replace("-", "_")}.properties");
                            using(var writer = entry.Open())
                            {
                                var bt = resourceStream.ToArray();
                                await writer.WriteAsync(bt, 0, bt.Length);
                            }
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    await ms.ReadAsync(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "properties.zip");
            }
        }
        /// <summary>
        /// 导出xml文件
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet("Resources/xml/{projectId}")]
        public async Task<IActionResult> ExportXml(string projectId, string? cultureName)
        {
            var result = await openApiAppService.GetResources(projectId, cultureName, string.IsNullOrWhiteSpace(cultureName));
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in result.Data)
                    {
                        using(var resourceStream = new MemoryStream())
                        {
                            var sw = new StreamWriter(resourceStream);
                            await sw.WriteLineAsync("<resources>");
                            foreach (var r in resource.Resources)
                            {
                                sw.WriteLine($"     <string name=\"{r.Key}\">{r.Value}</string>");
                            }
                            await sw.WriteLineAsync("</resources>");
                            await sw.FlushAsync();
                            ZipArchiveEntry entry = zip.CreateEntry($"strings_{resource.CultureName}.xml");
                            using(var writer = entry.Open())
                            {
                                var bt = resourceStream.ToArray();
                                await writer.WriteAsync(bt, 0, bt.Length);
                            }
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    await ms.ReadAsync(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "xml.zip");
            }
        }
        /// <summary>
        /// 导出ts文件
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        [HttpGet("Resources/ts/{projectId}")]
        public async Task<IActionResult> ExportTs(string projectId, string? cultureName)
        {
            var result = await openApiAppService.GetResources(projectId, cultureName, string.IsNullOrWhiteSpace(cultureName));
            byte[] res;
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in result.Data)
                    {
                        using(var resourceStream = new MemoryStream())
                        {
                            var sw = new StreamWriter(resourceStream);
                            await sw.WriteLineAsync("export default {");
                            foreach (var r in resource.Resources)
                            {
                                sw.WriteLine($"  '{r.Key}': '{r.Value}',");
                            }
                            await sw.WriteLineAsync("};");
                            await sw.FlushAsync();
                            ZipArchiveEntry entry = zip.CreateEntry($"{resource.CultureName}.ts");
                            using(var writer = entry.Open())
                            {
                                var bt = resourceStream.ToArray();
                                await writer.WriteAsync(bt, 0, bt.Length);
                            }
                        }
                    }
                    InvokeWriteFile(zip);
                    int nowPos = (int)ms.Position;
                    res = new byte[ms.Length];
                    ms.Position = 0;
                    await ms.ReadAsync(res, 0, res.Length);
                    ms.Position = nowPos;
                }
                return File(res, "application/octet-stream", "ts.zip");
            }
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
