using LinguaNex.Const;
using Microsoft.Extensions.Options;
using SKIT.FlurlHttpClient.Baidu.Translate;
using Wheel.Core.Exceptions;

namespace LinguaNex.Translates.Baidu
{
    public class BaiduTranslate(IOptions<BaiduTranslateClientOptions> baiduTranslateClientOptions) : ITranslate
    {
        BaiduTranslateClient Client = new BaiduTranslateClient(baiduTranslateClientOptions.Value);
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
                From = ConvertLangCode(sourceLang),
                To = ConvertLangCode(targetLang)
            });
            if (response.IsSuccessful())
            {
                return response.ResultList.First().Destination;
            }
            throw new BusinessException(ErrorCode.TranslateError, ErrorCode.TranslateError).WithMessageDataData(response.ErrorCode.ToString(), response.ErrorMessage);
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
                _ => langCode
            };
        }
    }
}
