using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.OrderDtos;

namespace Services
{
    public class OrderService
        (IUnitOfWork unitOfWork , 
        IMapper mapper , 
        IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string buyerEmail)
        {
            // Address
            var address = mapper.Map<Address>(orderRequest.ShippingAddress);
            // Basket
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);

            if(basket is null)
                throw new BasketNotFoundException(orderRequest.BasketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product,int>().GetAsync(item.Id);

                if (product is null)
                    throw new ProductNotFoundException(item.Id);

                var productInOrderItem = new ProductInOrderItem(product.Id,product.Name,product.PictureUrl);

                var orderItem = new OrderItem(productInOrderItem, item.Quantity, product.Price);

                orderItems.Add(orderItem);

                // Delivery method
                var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);

                if (deliveryMethod is null)
                    throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

                // Subtotal
                var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

                var order = new Order(buyerEmail, address, orderItems, deliveryMethod, subtotal);

                await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);

                await unitOfWork.SaveChangesAsync();

                return mapper.Map<OrderResult>(order);
            }
        }

        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
