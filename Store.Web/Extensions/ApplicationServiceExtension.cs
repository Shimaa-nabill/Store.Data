using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.ProductService;

using Store.Service.HandleResponse;
using Store.Service.Services.CacheService;


using Store.Service.Services.ProductService.Dtos;
using Store.Service.Services.TokenService;
using Store.Service.Services.BasketService.DTOs;
using Store.Repository.Basket;
using Store.Service.Services.BasketService;
using Store.Service.Services.UserService;
using Store.Service.Services.OrderService.DTOs;
using Store.Service.Services.OrderService;
using Store.Service.Services.PaymentService;


namespace Store.Web.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(model => model.Value?.Errors.Count() > 0)
                                .SelectMany(model => model.Value.Errors)
                                .Select(error => error.ErrorMessage).ToList();

                    var errorRespone = new ValidationErrorResopnse { Errors = errors };

                    return new BadRequestObjectResult(errorRespone);
                };
            });
            return services;

        }
    }
}
