using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Translates.Tencent
{
    public class TencentTranslateClientOptions
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：秒）。
        /// </summary>
        public int TimeOut { get; set; } = 30;
        /// <summary>
        /// 获取或设置有道翻译开放平台 API 入口点。
        /// </summary>
        public string Endpoint { get; set; } = "tmt.tencentcloudapi.com";

        /// <summary>
        /// 获取或设置有道翻译 AppId。
        /// </summary>
        public string AppId { get; set; } = default!;

        /// <summary>
        /// 获取或设置有道翻译 AppSecret。
        /// </summary>
        public string AppSecret { get; set; } = default!;
    }
}
