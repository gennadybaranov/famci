using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Discount
{
    public interface IDiscount
    {
        decimal CalculateDiscount(OrderItem orderItem, decimal total);
    }
}
