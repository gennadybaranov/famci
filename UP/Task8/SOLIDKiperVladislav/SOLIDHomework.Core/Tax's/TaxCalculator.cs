using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core
{
    abstract class TaxCalculator
    {
        abstract public decimal ApplyTax(ref decimal price);
    }
}
