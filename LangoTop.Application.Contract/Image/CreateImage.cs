using Microsoft.AspNetCore.Http;

namespace LangoTop.Application.Contract.Image
{
    public class CreateImage
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Link { get; set; }
    }
}
