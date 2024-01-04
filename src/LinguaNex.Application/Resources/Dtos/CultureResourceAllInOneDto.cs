using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Resources.Dtos
{
    public class CultureResourceAllInOneDto
    {
        public string Key { get; set; }
        public Dictionary<string, string> Resources { get; set; }
    }
}
