using Azure.Core;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Docs;
using Services.Authenticate;
using Services.Docs.Requests;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<long> PutAsync(PutRequest request, IFormFile file)
        {
            request.ThrowIfNull(nameof(request));
            file.ThrowIfNull(nameof(file));
            
            Doc doc = new Doc(
                request.Name,
                request.DocTypeId,
                DateTimeOffset.Now,
                request.CreatedUserId,
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
