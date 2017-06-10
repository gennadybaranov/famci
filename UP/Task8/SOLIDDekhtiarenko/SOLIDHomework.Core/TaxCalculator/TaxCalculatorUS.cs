using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.TaxCalculator
{
    public class TaxCalculatorUS : ITaxCalculator
    {
        public decimal CalculateTax(decimal Price)
        {
            return Price * 0.13M;
        }
    }
}
