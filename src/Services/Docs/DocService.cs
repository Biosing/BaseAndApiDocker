using BaseAndApiDocker.Controllers;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Docs;
using Models.Dto.Docs;
using Services.Authenticate;
using Services.Docs.Requests;
using Services.Utils;
using Models.Utils;
using System.Security.Cryptography.X509Certificates;

namespace Services.Docs
{
    public class DocService : IDocService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthorization _auth;

        public DocService(DatabaseContext context, IAuthorization auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task DeleteAsync(long id)
        {
            id.ThrowIfNull(nameof(id));
            var authUserid = _auth.CurrentUser().Id;

            var doc = await _context.Docs
                .FirstOrDefaultAsync(x => x.Id == id);
                
            doc.HasOrFail()
                .IsOwner(authUserid);
            
            _context.Docs.Remove(doc);
            await _context.SaveChangesAsync();
        }

        public async Task<FileContentResult> GetAsync(long id)
        {
            id.ThrowIfNull(nameof(id));
            var authUserid = _auth.CurrentUser().Id;

            var doc = await _context.Docs.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            doc.HasOrFail()
            .IsOwnerOrRecipient(authUserid);

            
            return new FileContentResult(Convert.FromBase64String(doc.Content), "application/octet-stream") { FileDownloadName = doc.Name };
        }

        public async Task<IReadOnlyCollection<ListDto>> ListAsync(ListRequest request)
        {
            request.ThrowIfNull(nameof(request));
            var authUserid = _auth.CurrentUser().Id;
            IQueryable<Doc> query = _context.Docs;

            return await query
                .When(request.StartDate.HasValue, x => x.CreatedDate >= request.StartDate.Value)
                .When(request.EndDate.HasValue, x => x.CreatedDate <= request.EndDate.Value)
                .Where(x => x.CreatedUserId == authUserid || x.ReceiverUserId == authUserid)
                .OrderBy(x => x.Name)
                .AllAsync(x => new ListDto(x));
        }

        public async Task<long> PutAsync(PutRequest request, IFormFile file)
        {
            request.ThrowIfNull(nameof(request));
            file.ThrowIfNull(nameof(file));
            var authUserid = _auth.CurrentUser().Id;

            Doc doc = new Doc(
                file.FileName,
                request.DocTypeId,
                DateTimeOffset.Now,
                authUserid,
                request.ReceiverUserId,
                ContentToBase64(file));

            await _context.Docs.AddAsync(doc);
            await _context.SaveChangesAsync();

            return doc.Id;
        }

        
        private string ContentToBase64(IFormFile content)
        {       
            if (content.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    content.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }
            return String.Empty;
        }
    }
}
