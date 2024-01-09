namespace LinguaNex.Translates
{
    public interface ITranslate
    {
        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="sourceString">翻译字符串</param>
        /// <param name="sourceLang">翻译字符串语言</param>
        /// <param name="targetLang">目标语言</param>
        /// <returns></returns>
        Task<string> Translate(string sourceString, string sourceLang, string targetLang);
    }
}
