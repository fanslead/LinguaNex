using LinguaNex.Translates.YouDao.Dtos;
using System.Security.Cryptography;
using System.Text;

namespace LinguaNex.Translates.YouDao.Utilities
{
    public static class AuthV3Util
    {

        /*
            添加鉴权相关参数 -
            appKey : 应用ID
            salt : 随机值
            curtime : 当前时间戳(秒)
            signType : 签名版本
            sign : 请求签名
            
            @param appKey    您的应用ID
            @param appSecret 您的应用密钥
            @param paramsMap 请求参数表
        */
        public static void AddAuthParams(string appKey, string appSecret, Dictionary<string, string[]> paramsMap)
        {
            string q = "";
            string[] qArray;
            if (paramsMap.ContainsKey("q"))
            {
                qArray = paramsMap["q"];
            }
            else
            {
                qArray = paramsMap["img"];
            }
            foreach (var item in qArray)
            {
                q += item;
            }
            string salt = System.Guid.NewGuid().ToString();
            string curtime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "";
            string sign = CalculateSign(appKey, appSecret, q, salt, curtime);
            paramsMap.Add("appKey", new string[] { appKey });
            paramsMap.Add("salt", new string[] { salt });
            paramsMap.Add("curtime", new string[] { curtime });
            paramsMap.Add("signType", new string[] { "v3" });
            paramsMap.Add("sign", new string[] { sign });
        }

        /*
            计算鉴权签名 -
            计算方式 : sign = sha256(appKey + input(q) + salt + curtime + appSecret)
        
            @param appKey    您的应用ID
            @param appSecret 您的应用密钥
            @param q         请求内容
            @param salt      随机值
            @param curtime   当前时间戳(秒)
            @return 鉴权签名sign
        */
        public static string CalculateSign(string appKey, string appSecret, string q, string salt, string curtime)
        {
            string strSrc = appKey + GetInput(q) + salt + curtime + appSecret;
            return Encrypt(strSrc);
        }

        private static string Encrypt(string strSrc)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(strSrc);
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        private static string GetInput(string q)
        {
            if (q == null)
            {
                return "";
            }
            int len = q.Length;
            return len <= 20 ? q : q.Substring(0, 10) + len + q.Substring(len - 10, 10);
        }


        public static Dictionary<string, string[]> CreateRequestParams(YouDaoTranslateRequestDto dto)
        {
            return new Dictionary<string, string[]>() {
                { "q", new string[]{dto.QueryString}},
                {"from", new string[]{dto.From}},
                {"to", new string[]{dto.To}},
                //{"vocabId", new string[]{vocabId}}
            };
        }

    }
}