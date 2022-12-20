using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Services.Docs.Requests;

namespace Services.Docs
{
    public interface IDocService
    {
        Task<long> PutAsync(PutRequest request, IFormFile file);
    }
}
