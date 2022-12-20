using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Services.Docs.Requests;

namespace Services.Docs
{
    public interface IDocService
    {
        Task DeleteAsync(long id);
        Task<long> PutAsync(PutRequest request, IFormFile file);
    }
}
