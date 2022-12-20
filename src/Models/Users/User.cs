using Models.Docs;
using Models.Utils.I18N;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Models.Users
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        public string Login { get; protected set; }
        [Required]
<<<<<<< HEAD
        [MaxLength(12)]
        [MinLength(12)]
        public string IIN { get; protected set; }
        [Required]
        [MaxLength(128)]
        public string Password { get; protected set; }

=======
        [MaxLength(12, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        [MinLength(12, ErrorMessage = DataAnnotationErrorMessages.MinLength)]
        public string IIN { get; protected set; }
        [Required]
        [MaxLength(128, ErrorMessage = DataAnnotationErrorMessages.MaxLength)]
        public string Password { get; protected set; }

        public virtual ICollection<Doc> CreatedDocs { get; protected set; }
        public virtual ICollection<Doc> ReceivedDocs { get; protected set; }

>>>>>>> dev
        protected User()
        {
        }

        public User(string login, string iin, string password)
        {
            Login = login;
            IIN = iin;
            Password = password;
        }
    }
}