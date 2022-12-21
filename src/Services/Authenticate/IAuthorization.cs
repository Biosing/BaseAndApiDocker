using Models.Users;

namespace Services.Authenticate
{
    public interface IAuthorization
    {
        CurrentUser CurrentUser();
    }
}
