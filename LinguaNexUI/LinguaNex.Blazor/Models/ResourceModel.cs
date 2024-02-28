using LinguaNex.Blazor.Dto;
using System.ComponentModel.DataAnnotations;

namespace LinguaNex.Blazor.Models
{
    public class ResourceModel
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string CultureId { get; set; }
        [Required]
        public bool Translate { get; set; }
        [Required]
        public bool SyncCulture { get; set; }

        [Required]
        public TranslateProviderEnum TranslateProvider { get; set; }
    }
}
