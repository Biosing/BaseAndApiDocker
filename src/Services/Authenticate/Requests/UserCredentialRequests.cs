using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate.Requests
{
    public class UserCredentialRequests
    {
        [Required(ErrorMessage = "Поле Логин обязателено для заполнения")]
        public string IIN { get; init; }
        [Required(ErrorMessage = "Поле Пароль обязателено для заполнения")]
        public string Password { get; init; }
    }
}
