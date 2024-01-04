using LinguaNex.Const;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SKIT.FlurlHttpClient.Baidu.Translate;
using Wheel.Core.Exceptions;

namespace LinguaNex.Translates.Baidu
{
    public class BaiduTranslate(ILogger<BaiduTranslate> logger,BaiduTranslateClient Client) : ITranslate
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
            var response = await Client.ExecuteTranslateVipTranslateAsync(new SKIT.FlurlHttpClient.Baidu.Translate.Models.TranslateVipTranslateRequest
            {
                QueryString = sourceString,
                From = "auto",//ConvertLangCode(sourceLang),
                To = ConvertLangCode(targetLang)
            });
            if (response.IsSuccessful())
            {
                return response.ResultList.First().Destination;
            }
            logger.LogError($"BaiduTranslate error: {response.ErrorCode}-{response.ErrorMessage}");
            return sourceString;
        }

        private string ConvertLangCode(string langCode)
        {
            return langCode switch
            {
                "zh-Hans" => "zh",
                "zh-Hant" => "zht",
                "en" => "en",
                "ja" => "jp",
                "ko" => "kor",
                "ar" => "ara",
                "fr" => "fra",
                "es" => "spa",
                "am" => "amh",
                "ba" => "bak",
                "eo" => "epo",
                _ => langCode
            };
        }
    }
}
