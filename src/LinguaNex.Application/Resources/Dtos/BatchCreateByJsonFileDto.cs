using Microsoft.AspNetCore.Http;

namespace LinguaNex.Resources.Dtos
{
    public class BatchCreateByJsonFileDto
    {
        public IFormFile File { get; set; }
    }
}
