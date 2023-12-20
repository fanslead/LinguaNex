using LinguaNex.Translates.Dto;

namespace LinguaNex.Translates
{
    public interface ITranslateAppService
    {
        Task<string> Translate(TranslateRequestDto dto);
    }
}
