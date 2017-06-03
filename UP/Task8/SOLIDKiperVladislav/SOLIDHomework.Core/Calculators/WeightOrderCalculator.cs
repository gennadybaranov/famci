using System.Linq;
using SOLIDHomework.Core.Discounts;

namespace SOLIDHomework.Core.Calculators
{
    public class WeightOrderCalculator : BaseOrderCalculator
    {
        private const decimal KG = 1000M;

        public override decimal Calculate(ShoppingCart shoppingCart)
        {
            return LoopShoppingCart(shoppingCart.OrderItems.Where(item => item.Type == "Weight")) / KG;
        }
    }
}
