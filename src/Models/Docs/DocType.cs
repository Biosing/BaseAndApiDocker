using Models.Utils.I18N;
using System.ComponentModel.DataAnnotations;

namespace Models.Docs
{
    public class DocType : BaseModel
    {
        [Required(ErrorMessage = DataAnnotationErrorMessages.Required)]
        [MaxLength(50, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        public string Name { get; protected set; }
        [MaxLength(255, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        public string Description { get; protected set; }

        protected DocType()
        {
        }

        public DocType(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}