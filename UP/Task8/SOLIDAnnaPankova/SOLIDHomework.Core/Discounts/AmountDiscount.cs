using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Discounts
{
    public class AmountDiscount : IDiscount
    {
        public bool AppliesTo(OrderItem item)
        {
            return item.Amount > 4;
        }

        public decimal RecalculateItemPrice(decimal price)
        {
            return price * (1 - 5 / 100M);
        }
    }
}
