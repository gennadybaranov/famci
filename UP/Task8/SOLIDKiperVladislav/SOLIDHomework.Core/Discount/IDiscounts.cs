using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Discount
{
    public interface IDiscount
    {
        void ApplyDiscount(OrderItem item, ref decimal productPrice);
    }

    public class ItemDiscount : IDiscount
    {
        public void ApplyDiscount(OrderItem item, ref decimal productPrice)
        {
            if (item.Amount > 4)
            {
                productPrice = productPrice * 0.95M;
            }
        }
    }

    public class SeasonEndDiscount : IDiscount
    {
        public void ApplyDiscount(OrderItem item, ref decimal productPrice)
        {
            if (item.SeasonEndDate <= DateTime.Now)
            {
                productPrice = productPrice * (1 - 20 / 100M);
            }
        }
    }
}
