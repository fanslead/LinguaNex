namespace LinguaNex.Const
{
    public class SupportedCulture
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string EnglishName { get; set; }

        private static Dictionary<string, string> _chineseLanguages = All().ToDictionary(a => a.Name, a => a.DisplayName);
        private static Dictionary<string, string> _englishLanguages = All().ToDictionary(a => a.Name, a => a.EnglishName);

        public static Dictionary<string, string> ChineseLanguages => _chineseLanguages;
        public static Dictionary<string, string> EnglishLanguages => _englishLanguages;



        public static List<SupportedCulture> All()
        {
            return new List<SupportedCulture> {
              new SupportedCulture
              {
                  DisplayName = "中文（简体）",
                  Name = "zh-Hans",
                  EnglishName = "Chinese (Simplified)"
              },
              new SupportedCulture
              {
                  DisplayName = "中文（繁体）",
                  Name = "zh-Hant",
                  EnglishName = "Chinese (Traditional)"
              },
              new SupportedCulture
              {
                  DisplayName = "英语",
                  Name = "en",
                  EnglishName = "English"
              },
              new SupportedCulture
              {
                  DisplayName = "阿法尔语",
                  Name = "aa",
                  EnglishName = "Afar"
              },
              new SupportedCulture
              {
                  DisplayName = "南非荷兰语",
                  Name = "af",
                  EnglishName = "Afrikaans"
              },
              new SupportedCulture
              {
                  DisplayName = "亚罕语",
                  Name = "agq",
                  EnglishName = "Aghem"
              },
              new SupportedCulture
              {
                  DisplayName = "阿肯语",
                  Name = "ak",
                  EnglishName = "Akan"
              },
              new SupportedCulture
              {
                  DisplayName = "阿姆哈拉语",
                  Name = "am",
                  EnglishName = "Amharic"
              },
              new SupportedCulture
              {
                  DisplayName = "阿拉伯语",
                  Name = "ar",
                  EnglishName = "Arabic"
              },
              new SupportedCulture
              {
                  DisplayName = "马普切语",
                  Name = "arn",
                  EnglishName = "Mapuche"
              },
              new SupportedCulture
              {
                  DisplayName = "阿萨姆语",
                  Name = "as",
                  EnglishName = "Assamese"
              },
              new SupportedCulture
              {
                  DisplayName = "帕雷语",
                  Name = "asa",
                  EnglishName = "Asu"
              },
              new SupportedCulture
              {
                  DisplayName = "阿斯图里亚斯语",
                  Name = "ast",
                  EnglishName = "Asturian"
              },
              new SupportedCulture
              {
                  DisplayName = "阿塞拜疆语",
                  Name = "az",
                  EnglishName = "Azerbaijani"
              },
              new SupportedCulture
              {
                  DisplayName = "阿塞拜疆语（西里尔文）",
                  Name = "az-Cyrl",
                  EnglishName = "Azerbaijani (Cyrillic)"
              },
              new SupportedCulture
              {
                  DisplayName = "阿塞拜疆语（拉丁文）",
                  Name = "az-Latn",
                  EnglishName = "Azerbaijani (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "巴什基尔语",
                  Name = "ba",
                  EnglishName = "Bashkir"
              },
              new SupportedCulture
              {
                  DisplayName = "巴萨语",
                  Name = "bas",
                  EnglishName = "Basaa"
              },
              new SupportedCulture
              {
                  DisplayName = "白俄罗斯语",
                  Name = "be",
                  EnglishName = "Belarusian"
              },
              new SupportedCulture
              {
                  DisplayName = "本巴语",
                  Name = "bem",
                  EnglishName = "Bemba"
              },
              new SupportedCulture
              {
                  DisplayName = "贝纳语",
                  Name = "bez",
                  EnglishName = "Bena"
              },
              new SupportedCulture
              {
                  DisplayName = "保加利亚语",
                  Name = "bg",
                  EnglishName = "Bulgarian"
              },
              new SupportedCulture
              {
                  DisplayName = "比尼语",
                  Name = "bin",
                  EnglishName = "Edo"
              },
              new SupportedCulture
              {
                  DisplayName = "班巴拉语",
                  Name = "bm",
                  EnglishName = "Bamanankan"
              },
              new SupportedCulture
              {
                  DisplayName = "孟加拉语",
                  Name = "bn",
                  EnglishName = "Bangla"
              },
              new SupportedCulture
              {
                  DisplayName = "藏语",
                  Name = "bo",
                  EnglishName = "Tibetan"
              },
              new SupportedCulture
              {
                  DisplayName = "布列塔尼语",
                  Name = "br",
                  EnglishName = "Breton"
              },
              new SupportedCulture
              {
                  DisplayName = "博多语",
                  Name = "brx",
                  EnglishName = "Bodo"
              },
              new SupportedCulture
              {
                  DisplayName = "波斯尼亚语",
                  Name = "bs",
                  EnglishName = "Bosnian"
              },
              new SupportedCulture
              {
                  DisplayName = "波斯尼亚语（西里尔文）",
                  Name = "bs-Cyrl",
                  EnglishName = "Bosnian (Cyrillic)"
              },
              new SupportedCulture
              {
                  DisplayName = "波斯尼亚语（拉丁文）",
                  Name = "bs-Latn",
                  EnglishName = "Bosnian (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "比林语",
                  Name = "byn",
                  EnglishName = "Blin"
              },
              new SupportedCulture
              {
                  DisplayName = "加泰罗尼亚语",
                  Name = "ca",
                  EnglishName = "Catalan"
              },
              new SupportedCulture
              {
                  DisplayName = "查克玛语",
                  Name = "ccp",
                  EnglishName = "Chakma"
              },
              new SupportedCulture
              {
                  DisplayName = "车臣语",
                  Name = "ce",
                  EnglishName = "Chechen"
              },
              new SupportedCulture
              {
                  DisplayName = "宿务语",
                  Name = "ceb",
                  EnglishName = "Cebuano"
              },
              new SupportedCulture
              {
                  DisplayName = "奇加语",
                  Name = "cgg",
                  EnglishName = "Chiga"
              },
              new SupportedCulture
              {
                  DisplayName = "切罗基语",
                  Name = "chr",
                  EnglishName = "Cherokee"
              },
              new SupportedCulture
              {
                  DisplayName = "中库尔德语",
                  Name = "ckb",
                  EnglishName = "Central Kurdish"
              },
              new SupportedCulture
              {
                  DisplayName = "科西嘉语",
                  Name = "co",
                  EnglishName = "Corsican"
              },
              new SupportedCulture
              {
                  DisplayName = "捷克语",
                  Name = "cs",
                  EnglishName = "Czech"
              },
              new SupportedCulture
              {
                  DisplayName = "教会斯拉夫语",
                  Name = "cu",
                  EnglishName = "Church Slavic"
              },
              new SupportedCulture
              {
                  DisplayName = "威尔士语",
                  Name = "cy",
                  EnglishName = "Welsh"
              },
              new SupportedCulture
              {
                  DisplayName = "丹麦语",
                  Name = "da",
                  EnglishName = "Danish"
              },
              new SupportedCulture
              {
                  DisplayName = "台塔语",
                  Name = "dav",
                  EnglishName = "Taita"
              },
              new SupportedCulture
              {
                  DisplayName = "德语",
                  Name = "de",
                  EnglishName = "German"
              },
              new SupportedCulture
              {
                  DisplayName = "哲尔马语",
                  Name = "dje",
                  EnglishName = "Zarma"
              },
              new SupportedCulture
              {
                  DisplayName = "多格拉语",
                  Name = "doi",
                  EnglishName = "Dogri"
              },
              new SupportedCulture
              {
                  DisplayName = "下索布语",
                  Name = "dsb",
                  EnglishName = "Lower Sorbian"
              },
              new SupportedCulture
              {
                  DisplayName = "杜阿拉语",
                  Name = "dua",
                  EnglishName = "Duala"
              },
              new SupportedCulture
              {
                  DisplayName = "迪维希语",
                  Name = "dv",
                  EnglishName = "Divehi"
              },
              new SupportedCulture
              {
                  DisplayName = "朱拉语",
                  Name = "dyo",
                  EnglishName = "Jola-Fonyi"
              },
              new SupportedCulture
              {
                  DisplayName = "宗卡语",
                  Name = "dz",
                  EnglishName = "Dzongkha"
              },
              new SupportedCulture
              {
                  DisplayName = "恩布语",
                  Name = "ebu",
                  EnglishName = "Embu"
              },
              new SupportedCulture
              {
                  DisplayName = "埃维语",
                  Name = "ee",
                  EnglishName = "Ewe"
              },
              new SupportedCulture
              {
                  DisplayName = "希腊语",
                  Name = "el",
                  EnglishName = "Greek"
              },
              new SupportedCulture
              {
                  DisplayName = "世界语",
                  Name = "eo",
                  EnglishName = "Esperanto"
              },
              new SupportedCulture
              {
                  DisplayName = "西班牙语",
                  Name = "es",
                  EnglishName = "Spanish"
              },
              new SupportedCulture
              {
                  DisplayName = "爱沙尼亚语",
                  Name = "et",
                  EnglishName = "Estonian"
              },
              new SupportedCulture
              {
                  DisplayName = "巴斯克语",
                  Name = "eu",
                  EnglishName = "Basque"
              },
              new SupportedCulture
              {
                  DisplayName = "埃翁多语",
                  Name = "ewo",
                  EnglishName = "Ewondo"
              },
              new SupportedCulture
              {
                  DisplayName = "波斯语",
                  Name = "fa",
                  EnglishName = "Persian"
              },
              new SupportedCulture
              {
                  DisplayName = "富拉语",
                  Name = "ff",
                  EnglishName = "Fulah"
              },
              new SupportedCulture
              {
                  DisplayName = "富拉语（阿德拉姆文）",
                  Name = "ff-Adlm",
                  EnglishName = "Fulah (Adlam)"
              },
              new SupportedCulture
              {
                  DisplayName = "富拉语（拉丁文）",
                  Name = "ff-Latn",
                  EnglishName = "Fulah (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "芬兰语",
                  Name = "fi",
                  EnglishName = "Finnish"
              },
              new SupportedCulture
              {
                  DisplayName = "菲律宾语",
                  Name = "fil",
                  EnglishName = "Filipino"
              },
              new SupportedCulture
              {
                  DisplayName = "法罗语",
                  Name = "fo",
                  EnglishName = "Faroese"
              },
              new SupportedCulture
              {
                  DisplayName = "法语",
                  Name = "fr",
                  EnglishName = "French"
              },
              new SupportedCulture
              {
                  DisplayName = "弗留利语",
                  Name = "fur",
                  EnglishName = "Friulian"
              },
              new SupportedCulture
              {
                  DisplayName = "西弗里西亚语",
                  Name = "fy",
                  EnglishName = "Western Frisian"
              },
              new SupportedCulture
              {
                  DisplayName = "爱尔兰语",
                  Name = "ga",
                  EnglishName = "Irish"
              },
              new SupportedCulture
              {
                  DisplayName = "苏格兰盖尔语",
                  Name = "gd",
                  EnglishName = "Scottish Gaelic"
              },
              new SupportedCulture
              {
                  DisplayName = "加利西亚语",
                  Name = "gl",
                  EnglishName = "Galician"
              },
              new SupportedCulture
              {
                  DisplayName = "瓜拉尼语",
                  Name = "gn",
                  EnglishName = "Guarani"
              },
              new SupportedCulture
              {
                  DisplayName = "瑞士德语",
                  Name = "gsw",
                  EnglishName = "Swiss German"
              },
              new SupportedCulture
              {
                  DisplayName = "古吉拉特语",
                  Name = "gu",
                  EnglishName = "Gujarati"
              },
              new SupportedCulture
              {
                  DisplayName = "古西语",
                  Name = "guz",
                  EnglishName = "Gusii"
              },
              new SupportedCulture
              {
                  DisplayName = "马恩语",
                  Name = "gv",
                  EnglishName = "Manx"
              },
              new SupportedCulture
              {
                  DisplayName = "豪萨语",
                  Name = "ha",
                  EnglishName = "Hausa"
              },
              new SupportedCulture
              {
                  DisplayName = "夏威夷语",
                  Name = "haw",
                  EnglishName = "Hawaiian"
              },
              new SupportedCulture
              {
                  DisplayName = "希伯来语",
                  Name = "he",
                  EnglishName = "Hebrew"
              },
              new SupportedCulture
              {
                  DisplayName = "印地语",
                  Name = "hi",
                  EnglishName = "Hindi"
              },
              new SupportedCulture
              {
                  DisplayName = "克罗地亚语",
                  Name = "hr",
                  EnglishName = "Croatian"
              },
              new SupportedCulture
              {
                  DisplayName = "上索布语",
                  Name = "hsb",
                  EnglishName = "Upper Sorbian"
              },
              new SupportedCulture
              {
                  DisplayName = "匈牙利语",
                  Name = "hu",
                  EnglishName = "Hungarian"
              },
              new SupportedCulture
              {
                  DisplayName = "亚美尼亚语",
                  Name = "hy",
                  EnglishName = "Armenian"
              },
              new SupportedCulture
              {
                  DisplayName = "国际语",
                  Name = "ia",
                  EnglishName = "Interlingua"
              },
              new SupportedCulture
              {
                  DisplayName = "伊比比奥语",
                  Name = "ibb",
                  EnglishName = "Ibibio"
              },
              new SupportedCulture
              {
                  DisplayName = "印度尼西亚语",
                  Name = "id",
                  EnglishName = "Indonesian"
              },
              new SupportedCulture
              {
                  DisplayName = "伊博语",
                  Name = "ig",
                  EnglishName = "Igbo"
              },
              new SupportedCulture
              {
                  DisplayName = "彝语",
                  Name = "ii",
                  EnglishName = "Yi"
              },
              new SupportedCulture
              {
                  DisplayName = "冰岛语",
                  Name = "is",
                  EnglishName = "Icelandic"
              },
              new SupportedCulture
              {
                  DisplayName = "意大利语",
                  Name = "it",
                  EnglishName = "Italian"
              },
              new SupportedCulture
              {
                  DisplayName = "因纽特语",
                  Name = "iu",
                  EnglishName = "Inuktitut"
              },
              new SupportedCulture
              {
                  DisplayName = "因纽特语（拉丁文）",
                  Name = "iu-Latn",
                  EnglishName = "Inuktitut (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "日语",
                  Name = "ja",
                  EnglishName = "Japanese"
              },
              new SupportedCulture
              {
                  DisplayName = "恩艮巴语",
                  Name = "jgo",
                  EnglishName = "Ngomba"
              },
              new SupportedCulture
              {
                  DisplayName = "马切姆语",
                  Name = "jmc",
                  EnglishName = "Machame"
              },
              new SupportedCulture
              {
                  DisplayName = "爪哇语",
                  Name = "jv",
                  EnglishName = "Javanese"
              },
              new SupportedCulture
              {
                  DisplayName = "爪哇语（爪哇文）",
                  Name = "jv-Java",
                  EnglishName = "Javanese (Javanese)"
              },
              new SupportedCulture
              {
                  DisplayName = "格鲁吉亚语",
                  Name = "ka",
                  EnglishName = "Georgian"
              },
              new SupportedCulture
              {
                  DisplayName = "卡拜尔语",
                  Name = "kab",
                  EnglishName = "Kabyle"
              },
              new SupportedCulture
              {
                  DisplayName = "卡姆巴语",
                  Name = "kam",
                  EnglishName = "Kamba"
              },
              new SupportedCulture
              {
                  DisplayName = "马孔德语",
                  Name = "kde",
                  EnglishName = "Makonde"
              },
              new SupportedCulture
              {
                  DisplayName = "卡布佛得鲁语",
                  Name = "kea",
                  EnglishName = "Kabuverdianu"
              },
              new SupportedCulture
              {
                  DisplayName = "西桑海语",
                  Name = "khq",
                  EnglishName = "Koyra Chiini"
              },
              new SupportedCulture
              {
                  DisplayName = "吉库尤语",
                  Name = "ki",
                  EnglishName = "Kikuyu"
              },
              new SupportedCulture
              {
                  DisplayName = "哈萨克语",
                  Name = "kk",
                  EnglishName = "Kazakh"
              },
              new SupportedCulture
              {
                  DisplayName = "卡库语",
                  Name = "kkj",
                  EnglishName = "Kako"
              },
              new SupportedCulture
              {
                  DisplayName = "格陵兰语",
                  Name = "kl",
                  EnglishName = "Kalaallisut"
              },
              new SupportedCulture
              {
                  DisplayName = "卡伦金语",
                  Name = "kln",
                  EnglishName = "Kalenjin"
              },
              new SupportedCulture
              {
                  DisplayName = "高棉语",
                  Name = "km",
                  EnglishName = "Khmer"
              },
              new SupportedCulture
              {
                  DisplayName = "卡纳达语",
                  Name = "kn",
                  EnglishName = "Kannada"
              },
              new SupportedCulture
              {
                  DisplayName = "韩语",
                  Name = "ko",
                  EnglishName = "Korean"
              },
              new SupportedCulture
              {
                  DisplayName = "孔卡尼语",
                  Name = "kok",
                  EnglishName = "Konkani"
              },
              new SupportedCulture
              {
                  DisplayName = "卡努里语",
                  Name = "kr",
                  EnglishName = "Kanuri"
              },
              new SupportedCulture
              {
                  DisplayName = "卡努里语（拉丁文）",
                  Name = "kr-Latn",
                  EnglishName = "Kanuri (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "克什米尔语",
                  Name = "ks",
                  EnglishName = "Kashmiri"
              },
              new SupportedCulture
              {
                  DisplayName = "克什米尔语（阿拉伯文）",
                  Name = "ks-Arab",
                  EnglishName = "Kashmiri (Arabic)"
              },
              new SupportedCulture
              {
                  DisplayName = "克什米尔语（天城文）",
                  Name = "ks-Deva",
                  EnglishName = "Kashmiri (Devanagari)"
              },
              new SupportedCulture
              {
                  DisplayName = "香巴拉语",
                  Name = "ksb",
                  EnglishName = "Shambala"
              },
              new SupportedCulture
              {
                  DisplayName = "巴菲亚语",
                  Name = "ksf",
                  EnglishName = "Bafia"
              },
              new SupportedCulture
              {
                  DisplayName = "科隆语",
                  Name = "ksh",
                  EnglishName = "Colognian"
              },
              new SupportedCulture
              {
                  DisplayName = "康沃尔语",
                  Name = "kw",
                  EnglishName = "Cornish"
              },
              new SupportedCulture
              {
                  DisplayName = "柯尔克孜语",
                  Name = "ky",
                  EnglishName = "Kyrgyz"
              },
              new SupportedCulture
              {
                  DisplayName = "拉丁语",
                  Name = "la",
                  EnglishName = "Latin"
              },
              new SupportedCulture
              {
                  DisplayName = "朗吉语",
                  Name = "lag",
                  EnglishName = "Langi"
              },
              new SupportedCulture
              {
                  DisplayName = "卢森堡语",
                  Name = "lb",
                  EnglishName = "Luxembourgish"
              },
              new SupportedCulture
              {
                  DisplayName = "卢干达语",
                  Name = "lg",
                  EnglishName = "Ganda"
              },
              new SupportedCulture
              {
                  DisplayName = "拉科塔语",
                  Name = "lkt",
                  EnglishName = "Lakota"
              },
              new SupportedCulture
              {
                  DisplayName = "林加拉语",
                  Name = "ln",
                  EnglishName = "Lingala"
              },
              new SupportedCulture
              {
                  DisplayName = "老挝语",
                  Name = "lo",
                  EnglishName = "Lao"
              },
              new SupportedCulture
              {
                  DisplayName = "北卢尔语",
                  Name = "lrc",
                  EnglishName = "Northern Luri"
              },
              new SupportedCulture
              {
                  DisplayName = "立陶宛语",
                  Name = "lt",
                  EnglishName = "Lithuanian"
              },
              new SupportedCulture
              {
                  DisplayName = "鲁巴加丹加语",
                  Name = "lu",
                  EnglishName = "Luba-Katanga"
              },
              new SupportedCulture
              {
                  DisplayName = "卢奥语",
                  Name = "luo",
                  EnglishName = "Luo"
              },
              new SupportedCulture
              {
                  DisplayName = "卢雅语",
                  Name = "luy",
                  EnglishName = "Luyia"
              },
              new SupportedCulture
              {
                  DisplayName = "拉脱维亚语",
                  Name = "lv",
                  EnglishName = "Latvian"
              },
              new SupportedCulture
              {
                  DisplayName = "迈蒂利语",
                  Name = "mai",
                  EnglishName = "Maithili"
              },
              new SupportedCulture
              {
                  DisplayName = "马赛语",
                  Name = "mas",
                  EnglishName = "Masai"
              },
              new SupportedCulture
              {
                  DisplayName = "梅鲁语",
                  Name = "mer",
                  EnglishName = "Meru"
              },
              new SupportedCulture
              {
                  DisplayName = "毛里求斯克里奥尔语",
                  Name = "mfe",
                  EnglishName = "Morisyen"
              },
              new SupportedCulture
              {
                  DisplayName = "马拉加斯语",
                  Name = "mg",
                  EnglishName = "Malagasy"
              },
              new SupportedCulture
              {
                  DisplayName = "马库阿语",
                  Name = "mgh",
                  EnglishName = "Makhuwa-Meetto"
              },
              new SupportedCulture
              {
                  DisplayName = "梅塔语",
                  Name = "mgo",
                  EnglishName = "Metaʼ"
              },
              new SupportedCulture
              {
                  DisplayName = "毛利语",
                  Name = "mi",
                  EnglishName = "Maori"
              },
              new SupportedCulture
              {
                  DisplayName = "马其顿语",
                  Name = "mk",
                  EnglishName = "Macedonian"
              },
              new SupportedCulture
              {
                  DisplayName = "马拉雅拉姆语",
                  Name = "ml",
                  EnglishName = "Malayalam"
              },
              new SupportedCulture
              {
                  DisplayName = "蒙古语",
                  Name = "mn",
                  EnglishName = "Mongolian"
              },
              new SupportedCulture
              {
                  DisplayName = "蒙古语（蒙古文）",
                  Name = "mn-Mong",
                  EnglishName = "Mongolian (Mongolian)"
              },
              new SupportedCulture
              {
                  DisplayName = "曼尼普尔语",
                  Name = "mni",
                  EnglishName = "Manipuri"
              },
              new SupportedCulture
              {
                  DisplayName = "曼尼普尔语（孟加拉文）",
                  Name = "mni-Beng",
                  EnglishName = "Manipuri (Bangla)"
              },
              new SupportedCulture
              {
                  DisplayName = "摩霍克语",
                  Name = "moh",
                  EnglishName = "Mohawk"
              },
              new SupportedCulture
              {
                  DisplayName = "马拉地语",
                  Name = "mr",
                  EnglishName = "Marathi"
              },
              new SupportedCulture
              {
                  DisplayName = "马来语",
                  Name = "ms",
                  EnglishName = "Malay"
              },
              new SupportedCulture
              {
                  DisplayName = "马耳他语",
                  Name = "mt",
                  EnglishName = "Maltese"
              },
              new SupportedCulture
              {
                  DisplayName = "蒙当语",
                  Name = "mua",
                  EnglishName = "Mundang"
              },
              new SupportedCulture
              {
                  DisplayName = "缅甸语",
                  Name = "my",
                  EnglishName = "Burmese"
              },
              new SupportedCulture
              {
                  DisplayName = "马赞德兰语",
                  Name = "mzn",
                  EnglishName = "Mazanderani"
              },
              new SupportedCulture
              {
                  DisplayName = "纳马语",
                  Name = "naq",
                  EnglishName = "Nama"
              },
              new SupportedCulture
              {
                  DisplayName = "书面挪威语",
                  Name = "nb",
                  EnglishName = "Norwegian Bokmål"
              },
              new SupportedCulture
              {
                  DisplayName = "北恩德贝勒语",
                  Name = "nd",
                  EnglishName = "North Ndebele"
              },
              new SupportedCulture
              {
                  DisplayName = "低地德语",
                  Name = "nds",
                  EnglishName = "Low German"
              },
              new SupportedCulture
              {
                  DisplayName = "尼泊尔语",
                  Name = "ne",
                  EnglishName = "Nepali"
              },
              new SupportedCulture
              {
                  DisplayName = "荷兰语",
                  Name = "nl",
                  EnglishName = "Dutch"
              },
              new SupportedCulture
              {
                  DisplayName = "夸西奥语",
                  Name = "nmg",
                  EnglishName = "Kwasio"
              },
              new SupportedCulture
              {
                  DisplayName = "挪威尼诺斯克语",
                  Name = "nn",
                  EnglishName = "Norwegian Nynorsk"
              },
              new SupportedCulture
              {
                  DisplayName = "恩甘澎语",
                  Name = "nnh",
                  EnglishName = "Ngiemboon"
              },
              new SupportedCulture
              {
                  DisplayName = "西非书面文字",
                  Name = "nqo",
                  EnglishName = "N’Ko"
              },
              new SupportedCulture
              {
                  DisplayName = "南恩德贝勒语",
                  Name = "nr",
                  EnglishName = "South Ndebele"
              },
              new SupportedCulture
              {
                  DisplayName = "北索托语",
                  Name = "nso",
                  EnglishName = "Sesotho sa Leboa"
              },
              new SupportedCulture
              {
                  DisplayName = "努埃尔语",
                  Name = "nus",
                  EnglishName = "Nuer"
              },
              new SupportedCulture
              {
                  DisplayName = "尼昂科勒语",
                  Name = "nyn",
                  EnglishName = "Nyankole"
              },
              new SupportedCulture
              {
                  DisplayName = "奥克语",
                  Name = "oc",
                  EnglishName = "Occitan"
              },
              new SupportedCulture
              {
                  DisplayName = "奥罗莫语",
                  Name = "om",
                  EnglishName = "Oromo"
              },
              new SupportedCulture
              {
                  DisplayName = "奥里亚语",
                  Name = "or",
                  EnglishName = "Odia"
              },
              new SupportedCulture
              {
                  DisplayName = "奥塞梯语",
                  Name = "os",
                  EnglishName = "Ossetic"
              },
              new SupportedCulture
              {
                  DisplayName = "旁遮普语",
                  Name = "pa",
                  EnglishName = "Punjabi"
              },
              new SupportedCulture
              {
                  DisplayName = "旁遮普语（阿拉伯文）",
                  Name = "pa-Arab",
                  EnglishName = "Punjabi (Arabic)"
              },
              new SupportedCulture
              {
                  DisplayName = "旁遮普语（果鲁穆奇文）",
                  Name = "pa-Guru",
                  EnglishName = "Punjabi (Gurmukhi)"
              },
              new SupportedCulture
              {
                  DisplayName = "帕皮阿门托语",
                  Name = "pap",
                  EnglishName = "Papiamento"
              },
              new SupportedCulture
              {
                  DisplayName = "尼日利亚皮钦语",
                  Name = "pcm",
                  EnglishName = "Nigerian Pidgin"
              },
              new SupportedCulture
              {
                  DisplayName = "波兰语",
                  Name = "pl",
                  EnglishName = "Polish"
              },
              new SupportedCulture
              {
                  DisplayName = "普鲁士语",
                  Name = "prg",
                  EnglishName = "Prussian"
              },
              new SupportedCulture
              {
                  DisplayName = "普什图语",
                  Name = "ps",
                  EnglishName = "Pashto"
              },
              new SupportedCulture
              {
                  DisplayName = "葡萄牙语",
                  Name = "pt",
                  EnglishName = "Portuguese"
              },
              new SupportedCulture
              {
                  DisplayName = "克丘亚语",
                  Name = "qu",
                  EnglishName = "Quechua"
              },
              new SupportedCulture
              {
                  DisplayName = "基切语",
                  Name = "quc",
                  EnglishName = "Kʼicheʼ"
              },
              new SupportedCulture
              {
                  DisplayName = "罗曼什语",
                  Name = "rm",
                  EnglishName = "Romansh"
              },
              new SupportedCulture
              {
                  DisplayName = "隆迪语",
                  Name = "rn",
                  EnglishName = "Rundi"
              },
              new SupportedCulture
              {
                  DisplayName = "罗马尼亚语",
                  Name = "ro",
                  EnglishName = "Romanian"
              },
              new SupportedCulture
              {
                  DisplayName = "兰博语",
                  Name = "rof",
                  EnglishName = "Rombo"
              },
              new SupportedCulture
              {
                  DisplayName = "俄语",
                  Name = "ru",
                  EnglishName = "Russian"
              },
              new SupportedCulture
              {
                  DisplayName = "卢旺达语",
                  Name = "rw",
                  EnglishName = "Kinyarwanda"
              },
              new SupportedCulture
              {
                  DisplayName = "罗瓦语",
                  Name = "rwk",
                  EnglishName = "Rwa"
              },
              new SupportedCulture
              {
                  DisplayName = "梵语",
                  Name = "sa",
                  EnglishName = "Sanskrit"
              },
              new SupportedCulture
              {
                  DisplayName = "萨哈语",
                  Name = "sah",
                  EnglishName = "Sakha"
              },
              new SupportedCulture
              {
                  DisplayName = "桑布鲁语",
                  Name = "saq",
                  EnglishName = "Samburu"
              },
              new SupportedCulture
              {
                  DisplayName = "桑塔利语",
                  Name = "sat",
                  EnglishName = "Santali"
              },
              new SupportedCulture
              {
                  DisplayName = "桑塔利语（桑塔利文）",
                  Name = "sat-Olck",
                  EnglishName = "Santali (Ol Chiki)"
              },
              new SupportedCulture
              {
                  DisplayName = "桑古语",
                  Name = "sbp",
                  EnglishName = "Sangu"
              },
              new SupportedCulture
              {
                  DisplayName = "信德语",
                  Name = "sd",
                  EnglishName = "Sindhi"
              },
              new SupportedCulture
              {
                  DisplayName = "信德语（阿拉伯文）",
                  Name = "sd-Arab",
                  EnglishName = "Sindhi (Arabic)"
              },
              new SupportedCulture
              {
                  DisplayName = "信德语（天城文）",
                  Name = "sd-Deva",
                  EnglishName = "Sindhi (Devanagari)"
              },
              new SupportedCulture
              {
                  DisplayName = "北方萨米语",
                  Name = "se",
                  EnglishName = "Northern Sami"
              },
              new SupportedCulture
              {
                  DisplayName = "塞纳语",
                  Name = "seh",
                  EnglishName = "Sena"
              },
              new SupportedCulture
              {
                  DisplayName = "东桑海语",
                  Name = "ses",
                  EnglishName = "Koyraboro Senni"
              },
              new SupportedCulture
              {
                  DisplayName = "桑戈语",
                  Name = "sg",
                  EnglishName = "Sango"
              },
              new SupportedCulture
              {
                  DisplayName = "希尔哈语",
                  Name = "shi",
                  EnglishName = "Tachelhit"
              },
              new SupportedCulture
              {
                  DisplayName = "希尔哈语（拉丁文）",
                  Name = "shi-Latn",
                  EnglishName = "Tachelhit (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "希尔哈语（提非纳文）",
                  Name = "shi-Tfng",
                  EnglishName = "Tachelhit (Tifinagh)"
              },
              new SupportedCulture
              {
                  DisplayName = "僧伽罗语",
                  Name = "si",
                  EnglishName = "Sinhala"
              },
              new SupportedCulture
              {
                  DisplayName = "斯洛伐克语",
                  Name = "sk",
                  EnglishName = "Slovak"
              },
              new SupportedCulture
              {
                  DisplayName = "斯洛文尼亚语",
                  Name = "sl",
                  EnglishName = "Slovenian"
              },
              new SupportedCulture
              {
                  DisplayName = "南萨米语",
                  Name = "sma",
                  EnglishName = "Southern Sami"
              },
              new SupportedCulture
              {
                  DisplayName = "吕勒萨米语",
                  Name = "smj",
                  EnglishName = "Lule Sami"
              },
              new SupportedCulture
              {
                  DisplayName = "伊纳里萨米语",
                  Name = "smn",
                  EnglishName = "Inari Sami"
              },
              new SupportedCulture
              {
                  DisplayName = "斯科特萨米语",
                  Name = "sms",
                  EnglishName = "Skolt Sami"
              },
              new SupportedCulture
              {
                  DisplayName = "绍纳语",
                  Name = "sn",
                  EnglishName = "Shona"
              },
              new SupportedCulture
              {
                  DisplayName = "索马里语",
                  Name = "so",
                  EnglishName = "Somali"
              },
              new SupportedCulture
              {
                  DisplayName = "阿尔巴尼亚语",
                  Name = "sq",
                  EnglishName = "Albanian"
              },
              new SupportedCulture
              {
                  DisplayName = "塞尔维亚语",
                  Name = "sr",
                  EnglishName = "Serbian"
              },
              new SupportedCulture
              {
                  DisplayName = "塞尔维亚语（西里尔文）",
                  Name = "sr-Cyrl",
                  EnglishName = "Serbian (Cyrillic)"
              },
              new SupportedCulture
              {
                  DisplayName = "塞尔维亚语（拉丁文）",
                  Name = "sr-Latn",
                  EnglishName = "Serbian (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "斯瓦蒂语",
                  Name = "ss",
                  EnglishName = "siSwati"
              },
              new SupportedCulture
              {
                  DisplayName = "萨霍语",
                  Name = "ssy",
                  EnglishName = "Saho"
              },
              new SupportedCulture
              {
                  DisplayName = "南索托语",
                  Name = "st",
                  EnglishName = "Sesotho"
              },
              new SupportedCulture
              {
                  DisplayName = "巽他语",
                  Name = "su",
                  EnglishName = "Sundanese"
              },
              new SupportedCulture
              {
                  DisplayName = "巽他语（拉丁文）",
                  Name = "su-Latn",
                  EnglishName = "Sundanese (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "瑞典语",
                  Name = "sv",
                  EnglishName = "Swedish"
              },
              new SupportedCulture
              {
                  DisplayName = "斯瓦希里语",
                  Name = "sw",
                  EnglishName = "Kiswahili"
              },
              new SupportedCulture
              {
                  DisplayName = "叙利亚语",
                  Name = "syr",
                  EnglishName = "Syriac"
              },
              new SupportedCulture
              {
                  DisplayName = "泰米尔语",
                  Name = "ta",
                  EnglishName = "Tamil"
              },
              new SupportedCulture
              {
                  DisplayName = "泰卢固语",
                  Name = "te",
                  EnglishName = "Telugu"
              },
              new SupportedCulture
              {
                  DisplayName = "特索语",
                  Name = "teo",
                  EnglishName = "Teso"
              },
              new SupportedCulture
              {
                  DisplayName = "塔吉克语",
                  Name = "tg",
                  EnglishName = "Tajik"
              },
              new SupportedCulture
              {
                  DisplayName = "泰语",
                  Name = "th",
                  EnglishName = "Thai"
              },
              new SupportedCulture
              {
                  DisplayName = "提格利尼亚语",
                  Name = "ti",
                  EnglishName = "Tigrinya"
              },
              new SupportedCulture
              {
                  DisplayName = "提格雷语",
                  Name = "tig",
                  EnglishName = "Tigre"
              },
              new SupportedCulture
              {
                  DisplayName = "土库曼语",
                  Name = "tk",
                  EnglishName = "Turkmen"
              },
              new SupportedCulture
              {
                  DisplayName = "茨瓦纳语",
                  Name = "tn",
                  EnglishName = "Setswana"
              },
              new SupportedCulture
              {
                  DisplayName = "汤加语",
                  Name = "to",
                  EnglishName = "Tongan"
              },
              new SupportedCulture
              {
                  DisplayName = "土耳其语",
                  Name = "tr",
                  EnglishName = "Turkish"
              },
              new SupportedCulture
              {
                  DisplayName = "聪加语",
                  Name = "ts",
                  EnglishName = "Xitsonga"
              },
              new SupportedCulture
              {
                  DisplayName = "鞑靼语",
                  Name = "tt",
                  EnglishName = "Tatar"
              },
              new SupportedCulture
              {
                  DisplayName = "北桑海语",
                  Name = "twq",
                  EnglishName = "Tasawaq"
              },
              new SupportedCulture
              {
                  DisplayName = "塔马齐格特语",
                  Name = "tzm",
                  EnglishName = "Central Atlas Tamazight"
              },
              new SupportedCulture
              {
                  DisplayName = "塔马齐格特语（阿拉伯文）",
                  Name = "tzm-Arab",
                  EnglishName = "Central Atlas Tamazight (Arabic)"
              },
              new SupportedCulture
              {
                  DisplayName = "塔马齐格特语（提非纳文）",
                  Name = "tzm-Tfng",
                  EnglishName = "Central Atlas Tamazight (Tifinagh)"
              },
              new SupportedCulture
              {
                  DisplayName = "维吾尔语",
                  Name = "ug",
                  EnglishName = "Uyghur"
              },
              new SupportedCulture
              {
                  DisplayName = "乌克兰语",
                  Name = "uk",
                  EnglishName = "Ukrainian"
              },
              new SupportedCulture
              {
                  DisplayName = "乌尔都语",
                  Name = "ur",
                  EnglishName = "Urdu"
              },
              new SupportedCulture
              {
                  DisplayName = "乌兹别克语",
                  Name = "uz",
                  EnglishName = "Uzbek"
              },
              new SupportedCulture
              {
                  DisplayName = "乌兹别克语（阿拉伯文）",
                  Name = "uz-Arab",
                  EnglishName = "Uzbek (Arabic)"
              },
              new SupportedCulture
              {
                  DisplayName = "乌兹别克语（西里尔文）",
                  Name = "uz-Cyrl",
                  EnglishName = "Uzbek (Cyrillic)"
              },
              new SupportedCulture
              {
                  DisplayName = "乌兹别克语（拉丁文）",
                  Name = "uz-Latn",
                  EnglishName = "Uzbek (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "瓦伊语",
                  Name = "vai",
                  EnglishName = "Vai"
              },
              new SupportedCulture
              {
                  DisplayName = "瓦伊语（拉丁文）",
                  Name = "vai-Latn",
                  EnglishName = "Vai (Latin)"
              },
              new SupportedCulture
              {
                  DisplayName = "瓦伊语（瓦依文）",
                  Name = "vai-Vaii",
                  EnglishName = "Vai (Vai)"
              },
              new SupportedCulture
              {
                  DisplayName = "文达语",
                  Name = "ve",
                  EnglishName = "Venda"
              },
              new SupportedCulture
              {
                  DisplayName = "越南语",
                  Name = "vi",
                  EnglishName = "VietNamese"
              },
              new SupportedCulture
              {
                  DisplayName = "沃拉普克语",
                  Name = "vo",
                  EnglishName = "Volapük"
              },
              new SupportedCulture
              {
                  DisplayName = "温旧语",
                  Name = "vun",
                  EnglishName = "Vunjo"
              },
              new SupportedCulture
              {
                  DisplayName = "瓦尔瑟语",
                  Name = "wae",
                  EnglishName = "Walser"
              },
              new SupportedCulture
              {
                  DisplayName = "瓦拉莫语",
                  Name = "wal",
                  EnglishName = "Wolaytta"
              },
              new SupportedCulture
              {
                  DisplayName = "沃洛夫语",
                  Name = "wo",
                  EnglishName = "Wolof"
              },
              new SupportedCulture
              {
                  DisplayName = "科萨语",
                  Name = "xh",
                  EnglishName = "isiXhosa"
              },
              new SupportedCulture
              {
                  DisplayName = "索加语",
                  Name = "xog",
                  EnglishName = "Soga"
              },
              new SupportedCulture
              {
                  DisplayName = "洋卞语",
                  Name = "yav",
                  EnglishName = "Yangben"
              },
              new SupportedCulture
              {
                  DisplayName = "意第绪语",
                  Name = "yi",
                  EnglishName = "Yiddish"
              },
              new SupportedCulture
              {
                  DisplayName = "约鲁巴语",
                  Name = "yo",
                  EnglishName = "Yoruba"
              },
              new SupportedCulture
              {
                  DisplayName = "标准摩洛哥塔马塞特语",
                  Name = "zgh",
                  EnglishName = "Standard Moroccan Tamazight"
              },
              new SupportedCulture
              {
                  DisplayName = "祖鲁语",
                  Name = "zu",
                  EnglishName = "isiZulu"
              }

                };
        }
    }
}
