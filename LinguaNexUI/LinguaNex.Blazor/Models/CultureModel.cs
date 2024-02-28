using LinguaNex.Blazor.Dto;
using System.ComponentModel.DataAnnotations;

namespace LinguaNex.Blazor.Models
{
    public class CultureModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool SyncResource { get; set; }

        [Required]
        public bool Translate { get; set; }

        [Required]
        public TranslateProviderEnum TranslateProvider { get; set; }
    }
}
