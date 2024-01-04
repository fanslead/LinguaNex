using LinguaNex.Translates.YouDao.Dtos;
using Microsoft.Extensions.Logging;

namespace LinguaNex.Translates.YouDao
{
    public class YouDaoTranslate(ILogger<YouDaoTranslate> logger, YouDaoTranslateClient client) : ITranslate
    {
        /// <summary>
        /// 有道翻译
        /// </summary>
        /// <param name="sourceString">翻译字符串</param>
        /// <param name="sourceLang">翻译字符串语言</param>
        /// <param name="targetLang">目标语言</param>
        /// <returns></returns>
        public async Task<string> Translate(string sourceString, string sourceLang, string targetLang)
        {
            var response = await client.YouDaoTranslate(new YouDaoTranslateRequestDto
            {
                QueryString = sourceString,
                From = ConvertLangCode(sourceLang),
                To = ConvertLangCode(targetLang)
            });
            if (response.ErrorCode == "0")
            {
                return response.Translation?.First();
            }
            logger.LogError($"YouDaoTranslate error: {response.ErrorCode}");
            return sourceString;
        }

        private string ConvertLangCode(string langCode)
        {
            return langCode switch
            {
                "zh-Hans" => "zh-CHS",
                "zh-Hant" => "zh-CHT",
                "en" => "en",
                "ja" => "ja",
                "ko" => "ko",
                "ar" => "ar",
                "fr" => "fra",
                "es" => "es",
                _ => langCode
            };
        }
    }
}
