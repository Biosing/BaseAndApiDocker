using Azure.Core;
using Database;
using Microsoft.AspNetCore.Http;
using Models.Docs;
using Services.Docs.Requests;
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

        public DocService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<long> PutAsync(PutRequest request, IFormFile file)
        {
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
