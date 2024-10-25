using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecifications(string? paymentIntentId)
            : base(order => order.PaymentIntentId == paymentIntentId) { }
    }
}
