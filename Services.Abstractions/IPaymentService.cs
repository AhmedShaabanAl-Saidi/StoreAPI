using Shared.BasketDtos;

namespace Services.Abstractions
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId);
        Task UpdateOrderPaymentAsync(string request,string stripeHeader);
    }
}
