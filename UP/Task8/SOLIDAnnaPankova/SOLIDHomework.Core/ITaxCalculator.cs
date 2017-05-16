using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core
{
    public interface ITaxCalculator
    {
        decimal GetTax(decimal price);
    }
}
