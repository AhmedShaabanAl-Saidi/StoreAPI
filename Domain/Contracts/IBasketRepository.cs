using Domain.Entities;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string Id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
