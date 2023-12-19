using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;

namespace LinguaNex.Cultures.Dtos
{
    public class CulturePageRequest : PageRequest
    {
        public string ProjectId { get; set; }
    }
}
