using LinguaNex.Aliyun;
using LinguaNex.Emuns;
using LinguaNex.Translates.AI;
using LinguaNex.Translates.Aliyun;
using LinguaNex.Translates.Baidu;
using LinguaNex.Translates.Tencent;
using LinguaNex.Translates.YouDao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using SKIT.FlurlHttpClient.Baidu.Translate;

namespace LinguaNex.Translates
{
    public static class TransaltesServiceCollectionExtension
    {
        public static IServiceCollection AddBaiduTransalte(this IServiceCollection services, Action<BaiduTranslateClientOptions> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var options = new BaiduTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton(sp => new BaiduTranslateClient(sp.GetRequiredService<BaiduTranslateClientOptions>()));
            
            services.AddKeyedSingleton<ITranslate, BaiduTranslate>(TranslateProviderEnum.Baidu.ToString());
            return services;
        }
        public static IServiceCollection AddAiTransalte(this IServiceCollection services, Action<IKernelBuilder> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var builder = Kernel.CreateBuilder();

            action.Invoke(builder);

            var kernel = builder.Build();

            services.AddSingleton(kernel);

            services.AddKeyedSingleton<ITranslate, AiTranslate>(TranslateProviderEnum.Ai.ToString());

            return services;
        }
        public static IServiceCollection AddYouDaoTransalte(this IServiceCollection services, Action<YouDaoTranslateClientOptions> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var options = new YouDaoTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<YouDaoTranslateClient>();

            services.AddKeyedSingleton<ITranslate, YouDaoTranslate>(TranslateProviderEnum.YouDao.ToString());

            return services;
        }
        public static IServiceCollection AddTencentTransalte(this IServiceCollection services, Action<TencentTranslateClientOptions> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var options = new TencentTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<TencentTranslateClient>();

            services.AddKeyedSingleton<ITranslate, TencentTranslate>(TranslateProviderEnum.Tencent.ToString());

            return services;
        }
        public static IServiceCollection AddAliyunTransalte(this IServiceCollection services, Action<AliyunTranslateClientOptions> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            var options = new AliyunTranslateClientOptions();
            action.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<AliyunTranslateClient>();

            services.AddKeyedSingleton<ITranslate, AliyunTranslate>(TranslateProviderEnum.Aliyun.ToString());

            return services;
        }
    }
}
