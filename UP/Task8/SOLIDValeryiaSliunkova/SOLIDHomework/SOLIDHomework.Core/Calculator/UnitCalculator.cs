using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public class UnitCalculator : BaseOrderCalculator
    {
        public override decimal Calculate(ShoppingCart shoppingCart)
        {
            return LoopShoppingCart(shoppingCart.OrderItems.Where(item => item.Type == "Unit"));
        }
    }
}
