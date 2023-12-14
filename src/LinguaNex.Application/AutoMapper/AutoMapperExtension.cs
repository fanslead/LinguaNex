using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LinguaNex.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var abs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                        .Where(x => !x.Contains("Microsoft.") && !x.Contains("System."))
                        .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x))).ToArray();
            return services.AddAutoMapper(abs);
        }
    }
}
