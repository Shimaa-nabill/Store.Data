using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItem ProductItem { get; set; }
        public Guid OrderId { get; set; }
    }
}
