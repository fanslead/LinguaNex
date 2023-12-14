using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Project.Dtos
{
    public class CreateProjectDto
    {
        public required string Name { get; set; }
        public bool Enalbe { get; set; }
    }
}
