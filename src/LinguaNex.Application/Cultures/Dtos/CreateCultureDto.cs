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
    }
}
