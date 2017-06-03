using System.Linq;

namespace SOLIDHomework.Core.Calculators
{
    public class SpecialOrderCalculator : BaseOrderCalculator
    {
        public override decimal Calculate(ShoppingCart shoppingCart)
        {
            return LoopShoppingCart(shoppingCart.OrderItems.Where(item => item.Type == "Special"));
        }
    }
}
