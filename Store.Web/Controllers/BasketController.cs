using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.DTOs;

namespace Store.Web.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string Id)
            => Ok(await _basketService.GetBasketAsync(Id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto input)
            => Ok(await _basketService.UpdateBasketAsync(input));

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Id)
            => Ok(await _basketService.DeleteBasketAsync(Id));
    }
}
