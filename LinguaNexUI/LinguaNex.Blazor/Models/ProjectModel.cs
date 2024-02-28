using System.ComponentModel.DataAnnotations;

namespace LinguaNex.Blazor.Models
{
    public class ProjectModel
    {
        [Required]
        public string Name { get; set; }
    }
}
