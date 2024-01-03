using Wheel.Core.Dto;

namespace LinguaNex.Resources.Dtos
{
    public class ResourcePageRequest : PageRequest
    {
        public long CultureId { get; set; }
    }
}
