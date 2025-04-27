using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.BasketDtos;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string Id)
        => await basketRepository.DeleteBasketAsync(Id);

        public async Task<BasketDto> GetBasketAsync(string Id)
        {
            var basket = await basketRepository.GetBasketAsync(Id);
            return basket is null ? throw new BasketNotFoundException(Id) 
                : mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);
            return updatedBasket is null ? throw new Exception("Basket not updated")
                : mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
