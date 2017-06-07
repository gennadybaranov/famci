using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Discount
{
    public class PacketDiscount : IDiscount
    {
        private readonly int setAmount;

        public decimal CalculateDiscount(OrderItem item, decimal total)
        {
            int setsOfProducts = item.Amount / setAmount;
            total -= setsOfProducts * item.Price;
            return total;
        }
    }
}
