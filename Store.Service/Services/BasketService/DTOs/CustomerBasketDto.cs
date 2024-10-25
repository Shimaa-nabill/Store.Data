namespace Store.Service.Services.BasketService.DTOs
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemDto> basketItems { get; set; } = new List<BasketItemDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
