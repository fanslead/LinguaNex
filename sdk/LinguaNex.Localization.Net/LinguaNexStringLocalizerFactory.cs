using LinguaNex.Extensions.Localization.Json.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Globalization;

namespace LinguaNex.Extensions.Localization.Json
{
    using LinguaNex.Extensions.Localization.Json.Caching;

    public class LinguaNexStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceNamesCache _resourceNamesCache = new ResourceNamesCache();
        private readonly ConcurrentDictionary<string, LinguaNexStringLocalizer> _localizerCache = new ConcurrentDictionary<string, LinguaNexStringLocalizer>();
        private readonly ILoggerFactory _loggerFactory;

        private readonly string LinguaNexApiUrl;
        private readonly string Project;
        public LinguaNexStringLocalizerFactory(
            IOptions<LinguaNexLocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
        {
            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }
            LinguaNexApiUrl = localizationOptions.Value.LinguaNexApiUrl;
            Project = localizationOptions.Value.Project;

            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return _localizerCache.GetOrAdd($"culture={CultureInfo.CurrentUICulture.Name}", _ => CreateJsonStringLocalizer());
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return _localizerCache.GetOrAdd($"culture={CultureInfo.CurrentUICulture.Name}", _ =>
            {
                return CreateJsonStringLocalizer();
            });
        }

        protected virtual LinguaNexStringLocalizer CreateJsonStringLocalizer()
        {
            var resourceManager = new LinguaNexResourceManager(LinguaNexApiUrl, Project);
            var logger = _loggerFactory.CreateLogger<LinguaNexStringLocalizer>();

            return new LinguaNexStringLocalizer(resourceManager, _resourceNamesCache, logger);
        }
    }
}