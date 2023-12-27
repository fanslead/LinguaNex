using LinguaNex.Extensions.Localization.Json;
using LinguaNex.Extensions.Localization.Json.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LinguaNexLocalizationServiceCollectionExtensions
    {

        public static IServiceCollection AddLinguaNexLocalization(
           this IServiceCollection services,
           Action<LinguaNexLocalizationOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();

            AddLinguaNexLocalizationServices(services, setupAction);

            return services;
        }

        internal static void AddLinguaNexLocalizationServices(IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizerFactory, LinguaNexStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.TryAddTransient(typeof(IStringLocalizer), typeof(StringLocalizer));
        }

        internal static void AddLinguaNexLocalizationServices(
            IServiceCollection services,
            Action<LinguaNexLocalizationOptions> setupAction)
        {
            AddLinguaNexLocalizationServices(services);
            services.Configure(setupAction);
        }
    }
}