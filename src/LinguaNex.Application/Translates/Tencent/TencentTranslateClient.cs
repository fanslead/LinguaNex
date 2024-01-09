using LinguaNex.Translates.Tencent.Dtos;
using Microsoft.Extensions.Logging;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Tmt.V20180321;
using TencentCloud.Tmt.V20180321.Models;

namespace LinguaNex.Translates.Tencent
{
    public class TencentTranslateClient(ILogger<TencentTranslateClient> logger, TencentTranslateClientOptions options)
    {
        public async Task<TextTranslateResponse> Translate(TencentTranslateRequestDto dto)
        {
            try
            {
                var client = BuildClient();
                var result = await client.TextTranslate(new TextTranslateRequest() { Source = dto.From, SourceText = dto.QueryString, Target = dto.To, ProjectId = 0, UntranslatedText = "无" });
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"TencentTranslate error: {ex.Message}");
            }
            return new TextTranslateResponse() { Source = dto.From, Target = dto.To, TargetText = dto.QueryString };
        }

        private TmtClient BuildClient()
        {
            // 实例化一个认证对象，入参需要传入腾讯云账户密钥对secretId，secretKey。
            var cred = new Credential
            {
                SecretId = options.AppId,
                SecretKey = options.AppSecret
            };

            // 实例化一个client选项，可选的，没有特殊需求可以跳过
            ClientProfile clientProfile = new ClientProfile();
            // 指定签名算法(默认为HmacSHA256)
            clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
            // 第二个参数是地域信息，可以直接填写字符串ap-guangzhou，或者引用预设的常量，clientProfile是可选的
            return new TmtClient(cred, "ap-guangzhou", clientProfile);
        }
    }
}
