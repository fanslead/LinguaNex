using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;

namespace LinguaNex.Resources.Dtos
{
    public class ResourcePageRequest : PageRequest
    {
        public string CultureId { get; set; }
    }
}
