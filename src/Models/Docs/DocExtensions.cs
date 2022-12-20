using Models.Users;
using Models.Utils.I18N;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Docs
{
    public static class DocExtensions
    {
        public static Doc IsOwner(this Doc doc, long userId)
        {
            if (doc.CreatedUserId == userId)
            {
                return doc;
            }

            throw new InvalidOperationException(DataAnnotationErrorMessages.NotOwner);
        }

        public static Doc HasOrFail(this Doc doc)
        {
            if (doc != null)
            {
                return doc;
            }

            throw new InvalidOperationException(DataAnnotationErrorMessages.DocNotFound);
        }
    }
}
