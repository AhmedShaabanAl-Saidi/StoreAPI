namespace Shared.ProductDtos
{
    public class ProductSpecificationParams
    {
        private const int MAX_PAGE_SIZE = 10;
        private const int DEFAULT_PAGE_SIZE = 5;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public SortingOptions? Sort { get; set; }

        public int PageIndex { get; set; } = 1;
        private int _pageSize = DEFAULT_PAGE_SIZE;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
    }
}
