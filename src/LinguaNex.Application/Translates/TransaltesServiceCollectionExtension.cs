using LinguaNex.Translates.AI;
using LinguaNex.Translates.Baidu;
using LinguaNex.Translates.YouDao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
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
            services.AddSingleton(options);
            services.AddSingleton(sp => new BaiduTranslateClient(sp.GetRequiredService<BaiduTranslateClientOptions>()));

            services.AddSingleton<ITranslate, BaiduTranslate>();

            return services;
        }
        public static IServiceCollection AddAiTransalte(this IServiceCollection services)
        {
            //services.AddAzureOpenAIChatCompletion();
            //services.AddAzureOpenAITextGeneration();
            //services.AddOpenAIChatCompletion();
            //services.AddOpenAITextGeneration();
            //services.AddSingleton(sp => Kernel.CreateBuilder().Build());
            //services.AddKeyedSingleton<ITranslate, AiTranslate>("Ai")

            return services;
        }
        public static IServiceCollection AddYouDaoTransalte(this IServiceCollection services, Action<YouDaoTranslateClientOptions> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var options = new YouDaoTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<YouDaoTranslateClient>();

            services.AddSingleton<ITranslate, YouDaoTranslate>();

            return services;
        }
    }
}
