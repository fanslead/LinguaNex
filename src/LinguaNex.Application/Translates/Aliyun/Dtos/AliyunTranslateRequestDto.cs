﻿namespace LinguaNex.Translates.Aliyun.Dtos
{
    public class AliyunTranslateRequestDto
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
