using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.ProductDtos;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return mappedBrands;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationParams specifications)
        {
            var productSpec = new ProductWithFilterSpecification(specifications);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(productSpec);
            var mappedProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);
            var countSpecs = new ProductCountSpescification(specifications);
            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecs);

            return new PaginatedResult<ProductResultDto>
                (specifications.PageIndex, specifications.PageSize,totalCount,mappedProducts);
        }

        public async Task<IEnumerable<TypeRessultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var mappedTypes = mapper.Map<IEnumerable<TypeRessultDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithFilterSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specs);
            var mappedProduct = mapper.Map<ProductResultDto>(product);
            return mappedProduct;
        }
    }
}
