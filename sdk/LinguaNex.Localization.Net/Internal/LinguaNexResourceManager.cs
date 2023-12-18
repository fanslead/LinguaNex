using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinguaNex.Extensions.Localization.Json.Internal
{
    public class LinguaNexResourceManager
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _resourcesCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        private readonly HttpClient _httpClient;

        public LinguaNexResourceManager(string linguaNexApiUrl, string project)
        {
            if (linguaNexApiUrl == null)
                throw new ArgumentNullException(nameof(linguaNexApiUrl));
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            LinguaNexApiUrl = linguaNexApiUrl;
            Project = project;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(linguaNexApiUrl);
        }

        public string LinguaNexApiUrl { get; set; }
        public string Project { get; set; }

        public virtual ConcurrentDictionary<string, string> GetResourceSet(CultureInfo culture, bool tryParents)
        {
            LoadResources(culture.Name).ConfigureAwait(false).GetAwaiter().GetResult();

            if (!_resourcesCache.ContainsKey(culture.Name))
            {
                return null;
            }

            if (tryParents)
            {
                var allResources = new ConcurrentDictionary<string, string>();
                do
                {
                    if (_resourcesCache.TryGetValue(culture.Name, out ConcurrentDictionary<string, string> resources))
                    {
                        foreach (var entry in resources)
                        {
                            allResources.TryAdd(entry.Key, entry.Value);
                        }
                    }

                    culture = culture.Parent;
                } while (culture != CultureInfo.InvariantCulture);

                return allResources;
            }
            else
            {
                _resourcesCache.TryGetValue(culture.Name, out ConcurrentDictionary<string, string> resources);

                return resources;
            }
        }

        public virtual string GetString(string name)
        {
            var culture = CultureInfo.CurrentUICulture;
            GetResourceSet(culture, tryParents: true);

            if (_resourcesCache.Count == 0)
            {
                return null;
            }

            do
            {
                if (_resourcesCache.ContainsKey(culture.Name))
                {
                    if (_resourcesCache[culture.Name].TryGetValue(name, out string value))
                    {
                        return value;
                    }
                }

                culture = culture.Parent;
            } while (culture != culture.Parent);

            return null;
        }

        public virtual string GetString(string name, CultureInfo culture)
        {
            GetResourceSet(culture, tryParents: true);

            if (_resourcesCache.Count == 0)
            {
                return null;
            }

            if (!_resourcesCache.ContainsKey(culture.Name))
            {
                return null;
            }

            return _resourcesCache[culture.Name].TryGetValue(name, out string value)
                ? value
                : null;
        }

        private async Task LoadResources(string? cultureName, bool all = false)
        {
            var response = await _httpClient.GetStringAsync($"api/OpenApi/Resources/{Project}?cultureName={cultureName}&all={all}");
            var resources = JsonSerializer.Deserialize<List<LinguaNexResources>>(response);
            foreach (var resource in resources)
            {
                _resourcesCache.TryAdd(resource.CultureName, new ConcurrentDictionary<string, string>(resource.Resources));
            }
        }
    }
}
