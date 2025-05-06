using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var order = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
            => Ok(await serviceManager.OrderService.GetOrderByIdAsync(id));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrdersByEmail()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var orders = await serviceManager.OrderService.GetOrdersByEmailAsync(email);
            return Ok(orders);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        => Ok(await serviceManager.OrderService.GetDeliveryMethodsAsync());
    }
