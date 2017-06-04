using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Tax_s
{
    class TaxUS : TaxCalculator
    {
        public override decimal ApplyTax(ref decimal price)
        {
            return price*0.13M;
        }
    }
}
