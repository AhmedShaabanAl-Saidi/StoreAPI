using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        }

        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;
    }
}
