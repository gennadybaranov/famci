using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public class TotalCalculator : ICalculator
    {
        public TotalCalculator(List<BaseOrderCalculator> _calculators)
        {
            Calculators = _calculators;
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
