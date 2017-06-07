using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public interface ICalculator
    {
        decimal CalculateTotal(ShoppingCart shoppingCart);
    }
}
