using LinguaNex.Const;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace LinguaNex.Translates.AI
{
    public class AiTranslate(Kernel kernel) : ITranslate
    {
        // todo optimize: 
        const string TranslatePrompt = "下面我让你来充当翻译家，你的目标是把指定语言翻译成目标语言，请翻译时不要带翻译腔，而是要翻译得自然、流畅和地道，使用优美和高雅的表达方式。要求直接输出翻译后的内容，如ask:你好，answer:hello。请将下面这句话翻译成{0}对应语言：{1}";
        public async Task<string> Translate(string sourceString, string sourceLang, string targetLang)
        {
            //var TranslatePlugin = kernel.ImportPluginFromPromptDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Translates/AI/Plugins"));
            //todo optimize:
            var service = kernel.GetRequiredService<IChatCompletionService>();
            var chatHistory = new ChatHistory
            {
                new ChatMessageContent(AuthorRole.User, string.Format(TranslatePrompt, SupportedCulture.ChineseLanguages[targetLang] ,sourceString))
            };
            var result = await service.GetChatMessageContentAsync(chatHistory);
            //var result = await kernel.InvokeAsync<string>(TranslatePlugin["Translate"], new KernelArguments
            //{
            //    ["input"] = sourceString,
            //    ["targetLang"] = SupportedCulture.ChineseLanguages[targetLang]
            //});
            return result.ToString();
        }
    }
}
