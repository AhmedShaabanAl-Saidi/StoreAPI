using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.BasketDtos;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("{Id}")]
        public async Task<ActionResult<BasketDto>> Get(string Id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(Id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(Id);
            return NoContent();
        }
    }
}
