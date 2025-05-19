using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.IdentityDtos;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICacheService> _cacheService;

        public ServiceManager(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasketRepository basketRepository,
            UserManager<User> userManager,
            IOptions<JwtOptions> options,
            IConfiguration configuration,
            ICacheRepository cacheRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,mapper,options));
            _orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork, mapper,basketRepository));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, basketRepository , mapper, configuration));
            _cacheService = new Lazy<ICacheService>(() => new CacheService(cacheRepository));
        }

        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IOrderService OrderService => _orderService.Value;
        public IPaymentService PaymentService => _paymentService.Value;
        public ICacheService CacheService => _cacheService.Value;
    }
}
