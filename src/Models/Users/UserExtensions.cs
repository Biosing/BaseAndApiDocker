using Models.Utils.Cryptography;

namespace Models.Users
{
    public static class UserExtensions
    {
        public static HashedString HashedPassword(this User user) => new(user.Password);
    }
}
