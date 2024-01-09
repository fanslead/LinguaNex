using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LinguaNex.Translates.YouDao.Dtos
{
    /// <summary>
    /// 有道翻译查询参数
    /// </summary>
    public class YouDaoTranslateRequestDto : YouDaoTranslateRequestBaseDto
    {
        /// <summary>
        /// 待翻译文本
        /// </summary>
        [JsonProperty("q")]
        [JsonPropertyName("q")]
        public string? QueryString { get; set; }
        /// <summary>
        /// 源语言
        /// </summary>
        [JsonProperty("from")]
        [JsonPropertyName("from")]
        public string? From { get; set; }
        /// <summary>
        /// 目标语言
        /// </summary>
        [JsonProperty("to")]
        [JsonPropertyName("to")]
        public string? To { get; set; }
    }
}
