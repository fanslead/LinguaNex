using LinguaNex.Emuns;

namespace LinguaNex.Resources.Dtos
{
    public class TransateMultipleLanguagesDto
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 源字符串语言
        /// </summary>
        public string SourceLanguage { get; set; }
        /// <summary>
        /// 翻译内容
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 翻译提供商
        /// </summary>
        public TranslateProviderEnum TranslateProvider { get; set; }
    }
}
