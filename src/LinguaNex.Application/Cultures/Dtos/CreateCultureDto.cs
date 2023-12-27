using LinguaNex.Emuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Cultures.Dtos
{
    public class CreateCultureDto
    {
        public required string Name { get; set; }
        public virtual required string ProjectId { get; set; }
        public bool? SyncResource { get; set; } = false;
        public bool? Translate { get; set; } = false;
        public TranslateProviderEnum? TranslateProvider { get; set; }
    }
}
