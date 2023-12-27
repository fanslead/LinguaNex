using System.Collections.Generic;
using System.Globalization;

namespace LinguaNex.Extensions.Localization.Internal
{
    public interface IResourceStringProvider
    {
        IList<string> GetAllResourceStrings(CultureInfo culture, bool throwOnMissing);
    }
}
