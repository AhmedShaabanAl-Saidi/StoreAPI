using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    public class PaymentController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var basket = await serviceManager.PaymentService.CreateOrUpdatePaymentAsync(basketId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await serviceManager.PaymentService.UpdateOrderPaymentAsync(requestBody, Request.Headers["Stripe-Signature"]);
            return Ok(requestBody);
        }
    }
}
