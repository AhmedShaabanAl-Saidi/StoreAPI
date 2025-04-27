namespace Domain.Exceptions
{
    public class BasketNotFoundException(string id) 
        : NotFoundExecption($"Basket with id {id} was not found!")
    {
    }
}
