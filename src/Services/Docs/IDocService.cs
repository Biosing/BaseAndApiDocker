using BaseAndApiDocker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Models.Dto.Docs;
using Services.Docs.Requests;

namespace Services.Docs
{
    public interface IDocService
    {
        Task DeleteAsync(long id);
        Task<FileContentResult> GetAsync(long id);
        Task<long> PutAsync(PutRequest request, IFormFile file);
        Task<IReadOnlyCollection<ListDto>> ListAsync(ListRequest request);
    }
}
