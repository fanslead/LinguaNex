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
            var translates = ServiceProvider.GetServices<ITranslate>();
            ITranslate translate = null;
            switch (dto.TranslateProvider)
            {
                case Emuns.TranslateProviderEnum.Baidu:
                    translate = translates.First(a => a is BaiduTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("Baidu");
                    break;
                case Emuns.TranslateProviderEnum.YouDao:
                    translate = translates.First(a => a is YouDaoTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("YouDao");
                    break;
                case Emuns.TranslateProviderEnum.Tencent:
                    translate = translates.First(a => a is TencentTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("Tencent");
                    break;
                case Emuns.TranslateProviderEnum.Aliyun:
                    translate = translates.First(a => a is AliyunTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("Aliyun");
                    break;
                case Emuns.TranslateProviderEnum.Ai:
                    translate = translates.First(a => a is AiTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("Ai");
                    break;
                default:
                    break;
            }

            var result = await translate.Translate(dto.SourceString, dto.SourceLang, dto.TargetLang);
            await Task.Delay(200);
            Logger.LogInformation($"Translate result: {result}");
            return result;
        }
    }
}
