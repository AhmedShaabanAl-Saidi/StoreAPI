namespace Shared.ProductDtos
{
    public class ProductSpecificationParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public SortingOptions? Sort { get; set; }
    }
}
