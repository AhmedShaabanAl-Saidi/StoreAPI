using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDtos;

namespace Services.Specifications
{
    public class ProductWithFilterSpecification : Specification<Product>
    {
        public ProductWithFilterSpecification(int id) 
            :base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        public ProductWithFilterSpecification(ProductSpecificationParams specs)
            :base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId) &&
                              (!specs.TypeId.HasValue || product.TypeId == specs.TypeId) &&
                              (string.IsNullOrEmpty(specs.Search) || 
                               product.Name.ToLower().Contains(specs.Search.ToLower().Trim()))
            )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            if (specs.Sort is not null)
            {
                switch(specs.Sort)
                {
                    case SortingOptions.NameAsc:
                        SetOrderBy(product => product.Name);
                        break;
                    case SortingOptions.NameDesc:
                        SetOrderByDescending(product => product.Name);
                        break;
                    case SortingOptions.PriceAsc:
                        SetOrderBy(product => product.Price);
                        break;
                    case SortingOptions.PriceDesc:
                        SetOrderByDescending(product => product.Price);
                        break;
                    default:
                        SetOrderBy(product => product.Name);
                        break;
                }
            }
        }
    }
}
