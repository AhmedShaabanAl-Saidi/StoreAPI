using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDtos;

namespace Services.Specifications
{
    public class ProductCountSpescification : Specification<Product>
    {
        public ProductCountSpescification(ProductSpecificationParams specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId) &&
                              (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) &&
                              (string.IsNullOrEmpty(specs.Search) ||
                               product.Name.ToLower().Contains(specs.Search.ToLower().Trim()))
            )
        {
            
        }
    }
}
