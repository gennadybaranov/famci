using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Calculator
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly decimal taxAmount;

        public decimal CalculateTax(decimal sum)
        {
            return sum * taxAmount;
        }
    }
}
