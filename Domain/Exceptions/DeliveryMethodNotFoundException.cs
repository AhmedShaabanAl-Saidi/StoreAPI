namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int id) 
        : NotFoundExecption($"Delivery method with id: {id} not found.")
    {
    }
}
