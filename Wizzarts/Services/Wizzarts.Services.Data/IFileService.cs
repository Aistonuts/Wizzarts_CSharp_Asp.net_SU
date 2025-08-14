namespace Wizzarts.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task<bool> IsValidImage(IFormFile file);

        Task<string> Sanitize(string input);
    }
}
