using Microsoft.AspNetCore.Http;

namespace CapstoneProject.Application.Services.Abstractions
{
    public interface IPhotoService
    {
        string AddPhoto(IFormFile file);
    }
}
