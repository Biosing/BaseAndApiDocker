using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public string Login { get; protected set; }
        [Required]
        [MaxLength(12)]
        [MinLength(12)]
        public string IIN { get; protected set; }
        [Required]
        [MaxLength(128)]
        public string Password { get; protected set; }

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