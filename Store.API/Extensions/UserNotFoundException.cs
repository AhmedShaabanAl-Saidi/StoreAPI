using Domain.Exceptions;

namespace Store.API.Extensions
{
    public class UserNotFoundException(string email) 
        : NotFoundExecption($"User with email {email} not found")
    {
    }
}
