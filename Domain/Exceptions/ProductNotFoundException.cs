namespace Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundExecption
    {
        public ProductNotFoundException(int id) : base($"Product With Id {id} Not Found!")
        {

        }
    }
}
