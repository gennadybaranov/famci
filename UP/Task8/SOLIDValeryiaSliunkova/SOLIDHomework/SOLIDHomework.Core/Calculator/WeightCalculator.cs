using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public class WeightCalculator : BaseOrderCalculator
    {
        private const decimal KG = 1000M;

        public override decimal Calculate(ShoppingCart shoppingCart)
        {
            return LoopShoppingCart(shoppingCart.OrderItems.Where(item => item.Type == "Weight")) / KG;
        }
    }
}
