﻿namespace LinguaNex.Translates.Tencent.Dtos
{
    public class TencentTranslateRequestDto
    {
        /// <summary>
        /// 待翻译文本
        /// </summary>
        public string? QueryString { get; set; }
        /// <summary>
        /// 源语言
        /// </summary>
        public string? From { get; set; }
        /// <summary>
        /// 目标语言
        /// </summary>
        public string? To { get; set; }
    }
}
