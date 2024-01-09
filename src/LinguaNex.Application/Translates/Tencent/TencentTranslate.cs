using LinguaNex.Translates.Tencent.Dtos;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace LinguaNex.Translates.Tencent
{
    public class TencentTranslate(ILogger<TencentTranslate> logger, TencentTranslateClient Client) : ITranslate
    {
        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="sourceString">翻译字符串</param>
        /// <param name="sourceLang">翻译字符串语言</param>
        /// <param name="targetLang">目标语言</param>
        /// <returns></returns>
        public async Task<string> Translate(string sourceString, string sourceLang, string targetLang)
        {
            var response = await Client.Translate(new TencentTranslateRequestDto
            {
                QueryString = sourceString,
                From = "auto",//ConvertLangCode(sourceLang),
                To = ConvertLangCode(targetLang)
            });
            logger.LogError($"TencentTranslate error: {response.ToJson(new System.Text.Json.JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            })}");
            return response?.TargetText;
        }

        private string ConvertLangCode(string langCode)
        {
            return langCode switch
            {
                "zh-Hans" => "zh",
                "zh-Hant" => "zh-TW",
                "en" => "en",
                "ja" => "jp",
                "ko" => "ko",
                "ar" => "ar",
                "fr" => "fr",
                "es" => "es",
                "ba" => "bak",
                _ => langCode
            };
        }
    }
}
