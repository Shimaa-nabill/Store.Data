using Store.Repository.Basket.Models;
using Store.Service.Services.BasketService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
