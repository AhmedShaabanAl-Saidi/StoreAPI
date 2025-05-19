using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using Shared.ProductDtos;

namespace Presentation.Controllers
{
    //[Authorize]
    public class ProductController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]
        [RedisCache(20)]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecificationParams specs)
        {
            var products = await serviceManager.ProductService.GetAllProductAsync(specs);
            return Ok(products);
        }

        [HttpGet]
        [RedisCache(20)]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        [RedisCache(20)]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet]
        [RedisCache(20)]
        public async Task<ActionResult<IEnumerable<TypeRessultDto>>> GetAllTypes()
        {
            var types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
