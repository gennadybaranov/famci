using System;
using System.Collections.Generic;

namespace SOLIDHomework.Core.Calculators
{
    public class TotalCalculator : ICalculator
    {
        public TotalCalculator(List<BaseOrderCalculator> calculators)
        {
            Calculators = calculators;
        }

        public List<BaseOrderCalculator> Calculators { get; set; }

        public decimal CalculateTotal(ShoppingCart shoppingCart)
        {
            decimal total = 0;
            foreach (var orderCalculator in Calculators)
            {
                total += orderCalculator.Calculate(shoppingCart);
            }
            return total;
        }
    }
}
