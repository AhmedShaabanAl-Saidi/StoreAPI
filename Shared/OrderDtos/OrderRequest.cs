namespace Shared.OrderDtos
{
    public class OrderRequest
    {
        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
