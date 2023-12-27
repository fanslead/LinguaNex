using System;
using System.Collections.Generic;

namespace LinguaNex.Extensions.Localization.Caching
{
    public interface IResourceNamesCache
    {
        IList<string> GetOrAdd(string name, Func<string, IList<string>> valueFactory);
    }
}
