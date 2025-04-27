using Shared.BasketDtos;

namespace Services.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Id);
        Task<BasketDto> UpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
