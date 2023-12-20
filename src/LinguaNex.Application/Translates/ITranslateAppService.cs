using LinguaNex.Translates.Dto;
using Wheel.DependencyInjection;

namespace LinguaNex.Translates
{
    public interface ITranslateAppService : ITransientDependency
    {
        Task<string> Translate(TranslateRequestDto dto);
    }
}
