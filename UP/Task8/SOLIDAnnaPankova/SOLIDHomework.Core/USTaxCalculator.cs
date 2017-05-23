﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core
{
    public class USTaxCalculator : ITaxCalculator
    {
        public decimal GetTax(decimal price)
        {
            return price * 0.13M;
        }
    }
}
