namespace Domain.Exceptions
{
    public class UnAuthorizedException(string message = "Invalid Email Or Password") 
        : Exception(message)
    {
    }
}
