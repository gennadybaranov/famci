using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Discounts
{
    public class DiscountAmount : IDiscount
    {
        public decimal CalculateDiscountedPrice(decimal total)
        {
            return total * 0.8M;
        }

        public bool Approached(OrderItem item)
        {
            return item.Amount > 3;
        }
    }
}
