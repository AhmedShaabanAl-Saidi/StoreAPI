namespace Domain.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
