using LinguaNex.Translates.Baidu;
using Microsoft.Extensions.DependencyInjection;
using SKIT.FlurlHttpClient.Baidu.Translate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Translates
{
    public static class TransaltesServiceCollectionExtension
    {
        public static IServiceCollection AddBaiduTransalte(this IServiceCollection services, Action<BaiduTranslateClientOptions> action)
        {
            if(action == null) throw new ArgumentNullException("action");
            var options = new BaiduTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(new BaiduTranslateClient(options));

            services.AddKeyedSingleton<ITranslate, BaiduTranslate>("Baidu");

            return services;
        }
    }
}
