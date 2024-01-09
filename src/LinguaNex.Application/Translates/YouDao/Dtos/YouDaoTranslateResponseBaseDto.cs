using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LinguaNex.Translates.YouDao.Dtos
{
    public class YouDaoTranslateResponseBaseDto
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errorCode")]
        [JsonPropertyName("errorCode")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// 翻译内容
        /// </summary>
        [JsonProperty("translation")]
        [JsonPropertyName("translation")]
        public string[]? Translation { get; set; }
        /// <summary>
        /// 源语言和目标语言
        /// </summary>
        [JsonProperty("l")]
        [JsonPropertyName("l")]
        public string? L { get; set; }
    }
}
