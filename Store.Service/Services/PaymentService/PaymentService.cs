using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.DTOs;
using Store.Service.Services.OrderService.DTOs;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,
            IUnitOfWork unitOfWork, IBasketService basketService,
            IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            if(basket is null)
                throw new ArgumentNullException(nameof(basket));

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId!.Value);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Is NOT Provided");

            decimal shippingPrice = deliveryMethod.Price;

            foreach (var item in basket.basketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductID);

                if(item.Price != product.Price)
                    item.Price = product.Price;
            }

            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;


            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.basketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),
                };

                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);

            }

            await _basketService.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpectificationByIdAsync(spec);

            if (order == null)
                throw new Exception("Order Does Not Exist");

            order.OrderPaymentStatus = OrderPaymentStatus.Failed;

            _unitOfWork.Repository<Order, Guid>().Update(order);

            await _unitOfWork.CountAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;


        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderWithPaymentIntentSpecifications(paymentIntentId);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpectificationByIdAsync(spec);

            if (order == null)
                throw new Exception("Order Does Not Exist");

            order.OrderPaymentStatus = OrderPaymentStatus.Received;

            _unitOfWork.Repository<Order, Guid>().Update(order);

            await _unitOfWork.CountAsync();

            await _basketService.DeleteBasketAsync(order.BasketId!);

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;
        }
    }
}
