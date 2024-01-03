using LinguaNex.Translates.Baidu;
using LinguaNex.Translates.Dto;
using LinguaNex.Translates.YouDao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wheel.Services;

namespace LinguaNex.Translates
{
    public class TranslateAppService : LinguaNexServiceBase, ITranslateAppService
    {
        public async Task<string> Translate(TranslateRequestDto dto)
        {
            Logger.LogInformation($"Translate data: {dto.ToJson()}");
            var translates = ServiceProvider.GetServices<ITranslate>();
            ITranslate translate = null;
            switch (dto.TranslateProvider)
            {
                case Emuns.TranslateProviderEnum.Baidu:
                    translate = translates.First(a=> a is BaiduTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("Baidu");
                    break;
                case Emuns.TranslateProviderEnum.YouDao:
                    translate = translates.First(a => a is YouDaoTranslate);//ServiceProvider.GetRequiredKeyedService<ITranslate>("YouDao");
                    break;
                default:
                    break;
            }

            var result = await translate.Translate(dto.SourceString, dto.SourceLang, dto.TargetLang);

            Logger.LogInformation($"Translate result: {result}");
            return result;
        }
    }
}
