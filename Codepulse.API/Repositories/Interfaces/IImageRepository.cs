using Codepulse.Model;
using System.Net;

namespace Codepulse.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile formFile, BlogImage blogImage);
        Task<List<BlogImage>> GetAll();
    }
}
