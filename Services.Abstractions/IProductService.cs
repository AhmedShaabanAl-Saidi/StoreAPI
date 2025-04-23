using Shared;
using Shared.ProductDtos;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationParams specs);
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeRessultDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    }
}
