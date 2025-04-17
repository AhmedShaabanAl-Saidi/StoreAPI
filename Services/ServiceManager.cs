using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        }

        public IProductService ProductService => _productService.Value;
    }
}
