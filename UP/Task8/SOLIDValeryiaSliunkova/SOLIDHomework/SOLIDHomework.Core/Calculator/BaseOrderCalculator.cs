using SOLIDHomework.Core.Discount;
using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public class BaseOrderCalculator
    {
        public List<IDiscount> Discounts { get; set; }

        protected decimal ApplyDiscounts(decimal total, OrderItem orderItem)
        {
            if (Discounts != null)
            {
                foreach (var discount in Discounts)
                {
                    total = discount.CalculateDiscount(orderItem, total);
                }
            }
            return total;
        }

        protected decimal LoopShoppingCart(IEnumerable<OrderItem> orderItems)
        {
            decimal total = 0;
            foreach (var orderItem in orderItems)
            {
                var price = orderItem.Amount * orderItem.Price;
                price = ApplyDiscounts(price, orderItem);
                total += price;
            }
            return total;
        }

        public virtual decimal Calculate(ShoppingCart shoppingCart)
        {
            return LoopShoppingCart(shoppingCart.OrderItems);
        }
    }
}
