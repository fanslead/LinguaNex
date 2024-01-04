using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinguaNex.Translates.YouDao.Dtos
{
    public class YouDaoTranslateRequestBaseDto
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        [JsonProperty("appKey")]
        [JsonPropertyName("appKey")]
        public string? AppKey { get; set; }
        /// <summary>
        /// UUID
        /// </summary>
        [JsonProperty("salt")]
        [JsonPropertyName("salt")]
        public string? Salt { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("sign")]
        [JsonPropertyName("sign")]
        public string? Sign { get; set; }
        /// <summary>
        /// 签名类型
        /// </summary>
        [JsonProperty("signType")]
        [JsonPropertyName("signType")]
        public string? SignType { get; set; } = "v3";
        /// <summary>
        /// 当前UTC时间戳(秒)
        /// </summary>
        [JsonProperty("curtime")]
        [JsonPropertyName("curtime")]
        public string? CurTime { get; set; }
    }
}
