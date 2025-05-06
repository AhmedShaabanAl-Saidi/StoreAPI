using Shared.OrderDtos;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email);
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest,string buyerEmail);
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
