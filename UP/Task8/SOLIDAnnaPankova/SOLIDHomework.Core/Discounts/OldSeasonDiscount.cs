using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Discounts
{
    public class OldSeasonDiscount : IDiscount
    {
        public bool AppliesTo(OrderItem item)
        {
            return item.SeasonEndDate <= DateTime.Now;
        }

        public decimal RecalculateItemPrice(decimal price)
        {
            return price * (1 - 20 / 100M);
        }
    }
}
