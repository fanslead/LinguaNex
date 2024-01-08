using LinguaNex.Aliyun;
using LinguaNex.Translates.Aliyun.Dtos;
using Microsoft.Extensions.Logging;

namespace LinguaNex.Translates.Aliyun
{
    public class AliyunTranslate(ILogger<AliyunTranslate> logger, AliyunTranslateClient Client) : ITranslate
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
            var response = await Client.Translate(new AliyunTranslateRequestDto
            {
                QueryString = sourceString,
                From = "auto",//ConvertLangCode(sourceLang),
                To = ConvertLangCode(targetLang)
            });
            logger.LogError($"AliyunTranslate error: {response}");
            return response;
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
