using LinguaNex.Extensions.Localization.Json.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace LinguaNex.Extensions.Localization.Json
{
    using LinguaNex.Extensions.Localization.Json.Caching;

    public class LinguaNexStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, object> _missingManifestCache = new ConcurrentDictionary<string, object>();
        private readonly IResourceNamesCache _resourceNamesCache;
        private readonly LinguaNexResourceManager _linguaNexResourceManager;
        private readonly IResourceStringProvider _resourceStringProvider;
        private readonly ILogger _logger;

        private string _searchedLocation;

        public LinguaNexStringLocalizer(
            LinguaNexResourceManager linguaNexResourceManager,
            IResourceNamesCache resourceNamesCache,
            ILogger logger)
            : this(linguaNexResourceManager,
                new LinguaNexStringProvider(resourceNamesCache, linguaNexResourceManager),
                resourceNamesCache,
                logger)
        {
        }

        public LinguaNexStringLocalizer(
            LinguaNexResourceManager linguaNexResourceManager,
            IResourceStringProvider resourceStringProvider,
            IResourceNamesCache resourceNamesCache,
            ILogger logger)
        {
            _linguaNexResourceManager = linguaNexResourceManager ?? throw new ArgumentNullException(nameof(linguaNexResourceManager));
            _resourceStringProvider = resourceStringProvider ?? throw new ArgumentNullException(nameof(resourceStringProvider));
            _resourceNamesCache = resourceNamesCache ?? throw new ArgumentNullException(nameof(resourceNamesCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _searchedLocation = linguaNexResourceManager.Project;
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetStringSafely(name, null);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetStringSafely(name, null);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
            }
        }

        public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        public IStringLocalizer WithCulture(CultureInfo culture) => this;

        protected virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var resourceNames = includeParentCultures
                ? GetResourceNamesFromCultureHierarchy(culture)
                : _resourceStringProvider.GetAllResourceStrings(culture, true);

            foreach (var name in resourceNames)
            {
                var value = GetStringSafely(name, culture);
                yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        protected string GetStringSafely(string name, CultureInfo culture)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var keyCulture = culture ?? CultureInfo.CurrentUICulture;
            var cacheKey = $"name={name}&culture={keyCulture.Name}";

            _logger.SearchedLocation(name, _linguaNexResourceManager.Project, keyCulture);

            if (_missingManifestCache.ContainsKey(cacheKey))
            {
                return null;
            }

            try
            {
                return culture == null
                        ? _linguaNexResourceManager.GetString(name)
                        : _linguaNexResourceManager.GetString(name, culture);
            }
            catch (MissingManifestResourceException)
            {
                _missingManifestCache.TryAdd(cacheKey, null);

                return null;
            }
        }

        private IEnumerable<string> GetResourceNamesFromCultureHierarchy(CultureInfo startingCulture)
        {
            var currentCulture = startingCulture;
            var resourceNames = new HashSet<string>();

            while (currentCulture != currentCulture.Parent)
            {
                var cultureResourceNames = _resourceStringProvider.GetAllResourceStrings(currentCulture, false);

                if (cultureResourceNames != null)
                {
                    foreach (var resourceName in cultureResourceNames)
                    {
                        resourceNames.Add(resourceName);
                    }
                }

                currentCulture = currentCulture.Parent;
            }

            return resourceNames;
        }
    }
}
