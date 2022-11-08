using Entities;
using Entities.Dtos;

namespace Business
{
    public interface IAuthenticationBL
    {
        bool GetUser(string email, string key);
        Response NewUser(User user);
        Response Validate(User user);
    }
}