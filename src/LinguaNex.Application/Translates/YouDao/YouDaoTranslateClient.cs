using Flurl.Http;
using LinguaNex.Translates.YouDao.Dtos;
using LinguaNex.Translates.YouDao.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var flurlClient = new FlurlClient();
            var result = await flurlClient.Request(options.Endpoint).WithHeader("content-type", "application/x-www-form-urlencoded").PostJsonAsync(dto).ReceiveJson<YouDaoTranslateResponseBaseDto>();
            return result;
        }
    }
}
