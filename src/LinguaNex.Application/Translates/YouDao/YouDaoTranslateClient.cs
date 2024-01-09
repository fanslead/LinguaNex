using LinguaNex.Translates.YouDao.Dtos;
using LinguaNex.Translates.YouDao.Utilities;
using Newtonsoft.Json;
using System.Text;

namespace LinguaNex.Translates.YouDao
{
    public class YouDaoTranslateClient(YouDaoTranslateClientOptions options)
    {
        public async Task<YouDaoTranslateResponseBaseDto> YouDaoTranslate(YouDaoTranslateRequestDto dto)
        {
            dto.AppKey = options.AppId;
            dto.Salt = Guid.NewGuid().ToString();
            dto.CurTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "";
            dto.Sign = AuthV3Util.CalculateSign(options.AppSecret, options.AppSecret, dto.QueryString, dto.Salt, dto.CurTime);

            var paramsMap = createRequestParams(dto);
            // 添加鉴权相关参数
            AuthV3Util.AddAuthParams(options.AppId, options.AppSecret, paramsMap);
            Dictionary<string, string[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };

            // 请求api服务
            byte[] result = await DoPost(options.Endpoint, header, paramsMap, "application/json");
            if (result != null)
            {
                return JsonConvert.DeserializeObject<YouDaoTranslateResponseBaseDto>(System.Text.Encoding.UTF8.GetString(result));
            }
            return new YouDaoTranslateResponseBaseDto() { ErrorCode = "0", L = dto.QueryString, Translation = new string[] { dto.QueryString } };
        }

        private static Dictionary<string, string[]> createRequestParams(YouDaoTranslateRequestDto dto)
        {
            return new Dictionary<string, string[]>() {
                { "q", new string[]{dto.QueryString}},
                {"from", new string[]{dto.From}},
                {"to", new string[]{dto.To}},
                //{"vocabId", new string[]{vocabId}}
            };
        }
        private async Task<byte[]> DoPost(string url, Dictionary<String, String[]> header, Dictionary<String, String[]> param, string expectContentType)
        {
            try
            {
                StringBuilder content = new StringBuilder();
                using (HttpClient client = new HttpClient())
                {
                    if (param != null)
                    {
                        int i = 0;
                        foreach (var p in param)
                        {
                            foreach (var v in p.Value)
                            {
                                if (i > 0)
                                {
                                    content.Append("&");
                                }
                                content.AppendFormat("{0}={1}", p.Key, System.Web.HttpUtility.UrlEncode(v));
                                i++;
                            }

                        }
                    }

                    var para = new StringContent(content.ToString());
                    if (header != null)
                    {
                        para.Headers.Clear();
                        foreach (var h in header)
                        {
                            foreach (var v in h.Value)
                            {
                                para.Headers.Add(h.Key, v);
                            }
                        }
                    }
                    var res = client.PostAsync(url, para).Result;
                    IEnumerable<string> contentTypeHeader;
                    var suc = res.Content.Headers.TryGetValues("Content-Type", out contentTypeHeader);
                    if (suc && !((string[])contentTypeHeader)[0].Contains(expectContentType))
                    {
                        Console.WriteLine(await res.Content.ReadAsStringAsync());
                        return null;
                    }
                    return await res.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("http request error: " + ex);
                return null;
            }

        }
    }
}
