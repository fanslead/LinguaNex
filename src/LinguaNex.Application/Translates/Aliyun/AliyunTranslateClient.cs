using AlibabaCloud.SDK.Alimt20181012.Models;
using LinguaNex.Translates.Aliyun.Dtos;
using Newtonsoft.Json;

namespace LinguaNex.Aliyun
{
    public class AliyunTranslateClient(AliyunTranslateClientOptions options)
    {

        public async Task<string> Translate(AliyunTranslateRequestDto dto)
        {
            try
            {
                var client = BuildClient();
                var apiInfo = CreateApiInfo();
                var body = new Dictionary<string, object>() { };
                body["FormatType"] = "text";
                body["SourceLanguage"] = "auto";
                body["TargetLanguage"] = dto.To;
                body["SourceText"] = dto.QueryString;
                body["Scene"] = "general";
                // runtime options
                AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
                AlibabaCloud.OpenApiClient.Models.OpenApiRequest request = new AlibabaCloud.OpenApiClient.Models.OpenApiRequest
                {
                    Body = body,
                };
                // 复制代码运行请自行打印 API 的返回值
                // 返回值为 Map 类型，可从 Map 中获得三类数据：响应体 body、响应头 headers、HTTP 返回的状态码 statusCode。
                var result = client.CallApi(apiInfo, request, runtime);
                if (result != null)
                {
                    var dataJson = JsonConvert.SerializeObject(result);
                    var data = JsonConvert.DeserializeObject<TranslateResponse>(dataJson);
                    if (data.Body.Code == 200)
                    {
                        return data.Body.Data.Translated;
                    }
                    return data.Body.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
            return dto.QueryString;
        }

        private AlibabaCloud.OpenApiClient.Client BuildClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = options.AppId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = options.AppSecret,
            };
            // Endpoint 请参考 https://api.aliyun.com/product/alimt
            config.Endpoint = "mt.aliyuncs.com";
            return new AlibabaCloud.OpenApiClient.Client(config);
        }

        private AlibabaCloud.OpenApiClient.Models.Params CreateApiInfo()
        {
            AlibabaCloud.OpenApiClient.Models.Params params_ = new AlibabaCloud.OpenApiClient.Models.Params
            {
                // 接口名称
                Action = "TranslateGeneral",
                // 接口版本
                Version = "2018-10-12",
                // 接口协议
                Protocol = "HTTPS",
                // 接口 HTTP 方法
                Method = "POST",
                AuthType = "AK",
                Style = "RPC",
                // 接口 PATH
                Pathname = "/",
                // 接口请求体内容格式
                ReqBodyType = "formData",
                // 接口响应体内容格式
                BodyType = "json",
            };
            return params_;
        }
    }
}
