using LinguaNex.Emuns;

namespace LinguaNex.Translates.Dto
{
    public class TranslateRequestDto
    {
        public string SourceString { get; set; }
        public string SourceLang { get; set; }
        public string TargetLang { get; set; }
        public TranslateProviderEnum TranslateProvider { get; set; }
    }
}
