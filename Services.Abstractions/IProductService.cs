using Shared.ProductDtos;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<IEnumerable<TypeRessultDto>> GetAllTypesAsync();
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    }
}
