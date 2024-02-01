using LinguaNex.Translates.AI;
using LinguaNex.Translates.Aliyun;
using LinguaNex.Translates.Baidu;
using LinguaNex.Translates.Dto;
using LinguaNex.Translates.Tencent;
using LinguaNex.Translates.YouDao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Wheel.Services;

namespace LinguaNex.Translates
{
    public class TranslateAppService : LinguaNexServiceBase, ITranslateAppService
    {
        public async Task<string> Translate(TranslateRequestDto dto)
        {
            Logger.LogInformation($"Translate data: {dto.ToJson(new System.Text.Json.JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            })}");
            ITranslate translate = ServiceProvider.GetRequiredKeyedService<ITranslate>(dto.TranslateProvider.ToString());

            var result = await translate.Translate(dto.SourceString, dto.SourceLang, dto.TargetLang);
            await Task.Delay(200);
            Logger.LogInformation($"Translate result: {result}");
            return result;
        }
    }
}
