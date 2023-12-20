using LinguaNex.Translates.Dto;
using Microsoft.Extensions.DependencyInjection;
using Wheel.Services;

namespace LinguaNex.Translates
{
    public class TranslateAppService : LinguaNexServiceBase, ITranslateAppService
    {
        public async Task<string> Translate(TranslateRequestDto dto)
        {
            ITranslate translate = null;
            switch (dto.TranslateProvider)
            {
                case Emuns.TranslateProviderEnum.Baidu:
                    translate = ServiceProvider.GetRequiredKeyedService<ITranslate>("Baidu");
                    break;
                default:
                    break;
            }

            return await translate.Translate(dto.SourceString, dto.SourceLang, dto.TargetLang);
        }
    }
}
