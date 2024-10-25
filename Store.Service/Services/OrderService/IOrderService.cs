using Store.Data.Entities;
using Store.Service.Services.OrderService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrderForUserAsync(string buyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid id);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
