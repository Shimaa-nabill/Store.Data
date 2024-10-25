namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }


    }
}
