using Models.Docs;
using Models.Users;
using Models.Utils.I18N;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Docs
{
    public class ListDto
    {
        public string Name { get; init; }
        public long Number { get; init; }
        public long DocTypeId { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public long CreatedUserId { get; init; }
        public long ReceiverUserId { get; init; }

        public ListDto(Doc doc)
        {
            doc.ThrowIfNull(nameof(doc));
            
            Name = doc.Name;
            Number = doc.Number;
            DocTypeId = doc.DocTypeId;
            CreatedDate = doc.CreatedDate;
            CreatedUserId = doc.CreatedUserId;
            ReceiverUserId = doc.ReceiverUserId;
        }
    }
}
