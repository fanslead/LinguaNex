using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaNex.Project.Dtos
{
    public class DeleteProjectAssociationDto
    {
        public string MainProjectId { get; set; }
        public string AssociationProjectId { get; set; }
    }
}
