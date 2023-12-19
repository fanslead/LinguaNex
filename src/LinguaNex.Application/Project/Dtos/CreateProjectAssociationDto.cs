using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Project.Dtos
{
    public class CreateProjectAssociationDto
    {
        public string MainProjectId { get; set; }
        public List<string> AssociationProjectIds { get; set; }
    }
}
