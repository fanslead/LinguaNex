namespace LinguaNex.Translates.YouDao
{
    public class YouDaoTranslateClientOptions
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：毫秒）。
        /// </summary>
        public int TimeOut { get; set; } = 30000;
        /// <summary>
        /// 获取或设置有道翻译开放平台 API 入口点。
        /// </summary>
        public string Endpoint { get; set; } = "https://openapi.youdao.com/api";

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
