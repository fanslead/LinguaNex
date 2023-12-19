using LinguaNex.Domain;
using LinguaNex.Entities;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Wheel.DependencyInjection;

namespace Wheel.Localization
{
    public class EFStringLocalizerStore(IBasicRepository<Resource, int> resourceRepository)
        : IStringLocalizerStore, ISingletonDependency
    {
        public IEnumerable<LocalizedString> GetAllStrings()
        {
            var list = resourceRepository.GetListAsync(r => r.ProjectId == "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7" && r.Culture.Name == CultureInfo.CurrentCulture.Name, propertySelectors: r => r.Culture).ConfigureAwait(false).GetAwaiter().GetResult();
            return list
                .Select(r => new LocalizedString(r.Key, r.Value, r.Value == null));
        }

        public string GetString(string name)
        {
            var resource = resourceRepository.FindAsync(r => r.ProjectId == "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7" && r.Culture.Name == CultureInfo.CurrentCulture.Name).ConfigureAwait(false).GetAwaiter().GetResult();
            return resource?.Value;
        }
    }
}
