namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
    }
}
