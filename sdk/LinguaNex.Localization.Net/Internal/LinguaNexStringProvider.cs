using LinguaNex.Extensions.Localization.Json.Caching;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace LinguaNex.Extensions.Localization.Json.Internal
{
    public class LinguaNexStringProvider : IResourceStringProvider
    {
        private readonly IResourceNamesCache _resourceNamesCache;
        private readonly LinguaNexResourceManager _jsonResourceManager;

        public LinguaNexStringProvider(IResourceNamesCache resourceCache, LinguaNexResourceManager jsonResourceManager)
        {
            _jsonResourceManager = jsonResourceManager;
            _resourceNamesCache = resourceCache;
        }

        private string GetResourceCacheKey(CultureInfo culture)
        {
            return $"Culture={culture.Name}";
        }

        public IList<string> GetAllResourceStrings(CultureInfo culture, bool throwOnMissing)
        {
            var cacheKey = GetResourceCacheKey(culture);

            return _resourceNamesCache.GetOrAdd(cacheKey, _ =>
            {
                var resourceSet = _jsonResourceManager.GetResourceSet(culture, tryParents: false);
                if (resourceSet == null)
                {
                    if (throwOnMissing)
                    {
                        throw new MissingManifestResourceException($"The manifest resource for the culture '{culture.Name}' is missing.");
                    }
                    else
                    {
                        return null;
                    }
                }

                var names = new List<string>();
                foreach (var entry in resourceSet)
                {
                    names.Add(entry.Key);
                }

                return names;
            });
        }
    }
}
