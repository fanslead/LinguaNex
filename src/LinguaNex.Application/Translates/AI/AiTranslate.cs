using LinguaNex.Const;
using Microsoft.SemanticKernel;

namespace LinguaNex.Translates.AI
{
    public class AiTranslate(Kernel kernel) : ITranslate
    {
        // todo optimize: 
        const string TranslatePrompt = @"下面我让你来充当翻译家，你的目标是把指定语言翻译成目标语言，请翻译时不要带翻译腔，而是要翻译得自然、流畅和地道，使用优美和高雅的表达方式。请将下面这句话翻译成{0}对应语言：{1}";
        public async Task<string> Translate(string sourceString, string sourceLang, string targetLang)
        {
            //todo optimize:
            var result = await kernel.InvokePromptAsync(string.Format(TranslatePrompt, SupportedCulture.ChineseLanguages[targetLang], sourceString));
            return result.GetValue<string>();
        }
    }
}
