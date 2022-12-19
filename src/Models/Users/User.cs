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
        public string IIN { get; protected set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; protected set; }
    }
}