using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Domain.Dtos.RequestDto
{
    public class PhotoRequestDto
    {
        public IFormFile File { get; set; }
    }
}
