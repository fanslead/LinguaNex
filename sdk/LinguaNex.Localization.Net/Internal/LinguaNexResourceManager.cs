using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinguaNex.Extensions.Localization.Json.Internal
{
    public class LinguaNexResourceManager
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _resourcesCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();

        private readonly HttpClient _httpClient;
        private HubConnection connection;

        public LinguaNexResourceManager(string linguaNexApiUrl, string project, bool useWebSocket)
        {
            if (linguaNexApiUrl == null)
                throw new ArgumentNullException(nameof(linguaNexApiUrl));
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            LinguaNexApiUrl = linguaNexApiUrl;
            Project = project;
            UseWebSocket = useWebSocket;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(linguaNexApiUrl);
            if (useWebSocket)
            {
                InitWebocket(linguaNexApiUrl, project);
            }
        }

        public string LinguaNexApiUrl { get; set; }
        public string Project { get; set; }
        public bool UseWebSocket { get; set; }

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
        private static List<string> cultureHasFirstLoad = new List<string>();
        private async Task LoadResources(string? cultureName, bool all = false)
        {
            if (_resourcesCache.TryGetValue(cultureName, out var _) && cultureHasFirstLoad.Any(a => a == cultureName))
                return;
            List<LinguaNexResources> resources;
            if (UseWebSocket)
            {
                resources = await connection.InvokeAsync<List<LinguaNexResources>>("GetResources", Project, cultureName, all);
            }
            else
            {
                var response = await _httpClient.GetStringAsync($"api/OpenApi/Resources/{Project}?cultureName={cultureName}&all={all}");
                resources = JsonSerializer.Deserialize<List<LinguaNexResources>>(response);
            }
            foreach (var resource in resources)
            {
                if (_resourcesCache.TryGetValue(resource.CultureName, out var value))
                {
                    foreach (var r in resource.Resources)
                    {
                        value[r.Key] = r.Value;
                    }
                    _resourcesCache[resource.CultureName] = value;
                }
                else
                {
                    _resourcesCache[resource.CultureName] = new ConcurrentDictionary<string, string>(resource.Resources);
                }
            }
            cultureHasFirstLoad.Add(cultureName);
        }

        private void InitWebocket(string linguaNexApiUrl, string project)
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{linguaNexApiUrl}/hubs/LinguaNex?project={project}", Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
                .AddJsonProtocol()
                .WithAutomaticReconnect()
                .Build();

            connection.On<LinguaNexResources>("CreateOrUpdateResource", obj => 
            {
                if (_resourcesCache.TryGetValue(obj.CultureName, out var value))
                {
                    foreach (var resource in obj.Resources)
                    {
                        value[resource.Key] = resource.Value;
                    }
                    _resourcesCache[obj.CultureName] = value;
                }else
                {
                    _resourcesCache[obj.CultureName] = new ConcurrentDictionary<string, string>(obj.Resources);
                }
            });
            connection.StartAsync();
        }
    }
}
