using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Discount
{
    public class SeasonDiscount : IDiscount
    {
        private readonly decimal discountAmount;

        public decimal CalculateDiscount(OrderItem item, decimal total)
        {
            
            if (item.SeasonEndDate <= DateTime.Now)
            {
                total = total * (1 - discountAmount / 100M);
            }
            return total;
        }
    }
}
