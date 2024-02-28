using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LinguaNex.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(/*"http://47.119.20.111/"*/builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<CultureClient>();
            builder.Services.AddScoped<OpenApiClient>();
            builder.Services.AddScoped<ProjectsClient>();
            builder.Services.AddScoped<ResourcesClient>();
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

            await builder.Build().RunAsync();
        }
    }
}