using LinguaNex.Emuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.EventDatas
{
    public class CultrueSyncResourceAndTranslateEto
    {
        public string Id { get; set; }
        public bool SyncResource { get; set; } = false;
        public bool Translate { get; set; } = false;
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
