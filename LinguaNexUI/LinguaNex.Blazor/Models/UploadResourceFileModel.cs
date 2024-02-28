using LinguaNex.Blazor.Dto;
using System.ComponentModel.DataAnnotations;

namespace LinguaNex.Blazor.Models
{
    public class UploadResourceFileModel
    {
        [Required]
        public string CultureId { get; set; }
        [Required]
        public bool Translate { get; set; }
        [Required]
        public TranslateProviderEnum TranslateProvider { get; set; }
    }
}
