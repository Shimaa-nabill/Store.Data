using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.BasketService.DTOs
{
    public class BasketItemDto
    {
        [Required, Range(1, int.MaxValue)]
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        [Required, Range(0.1 , double.MaxValue, ErrorMessage = "Price Must Be More Than 0")]
        public decimal Price { get; set; }
        [Required, Range(1, 10)]
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
    }
}