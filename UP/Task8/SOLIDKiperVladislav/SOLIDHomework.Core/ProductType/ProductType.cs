using SOLIDHomework.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.ProductType
{
    public interface IProductType
    {
        bool DefineType(OrderItem orderItem);
        decimal GetPrice(OrderItem orderItem);
    }

    public class UnitType : IProductType
    {
        public bool DefineType(OrderItem orderItem)
        {
            return orderItem.Type == "Unit";
        }

        public decimal GetPrice(OrderItem orderItem)
        {
            return orderItem.Price;
        }

    }

    public class WeightType : IProductType
    {
        public bool DefineType(OrderItem orderItem)
        {
            return orderItem.Type == "Weight";
        }

        public decimal GetPrice(OrderItem orderItem)
        {
            return orderItem.Price*orderItem.Amount/1000M;
        }

    }


}
