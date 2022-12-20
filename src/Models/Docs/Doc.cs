using Models.Users;
using Models.Utils.I18N;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Docs
{
    public class Doc : BaseModel
    {
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        [MaxLength(50, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        public string Name { get; protected set; }
        
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public long Number { get; protected set; }
        
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public long DocTypeId { get; protected set; }
        public virtual DocType DocType { get; protected set; }
        
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        
        public DateTimeOffset CreatedDate { get; protected set; }
        
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public long CreatedUserId { get; protected set; }
        public virtual User CreatedUser { get; protected set; }
        
        public long ReceiverUserId { get; protected set; }
        public virtual User ReceiverUser { get; protected set; }


        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        public string Content { get; protected set; }
    }
}
