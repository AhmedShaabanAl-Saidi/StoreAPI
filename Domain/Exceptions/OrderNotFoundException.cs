namespace Domain.Exceptions
{
    public class OrderNotFoundException(Guid id) 
        : NotFoundExecption($"Order with id {id} not found")
    {
    }
}
