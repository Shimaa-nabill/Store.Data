using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.DTOs
{
    public class OrderItemDto
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }         
        public Guid OrderId { get; set; }
    }
}
