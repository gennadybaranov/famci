using System;
using System.Collections.Generic;
using System.Configuration;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Tax_s;
using SOLIDHomework.Core.ProductType;
using SOLIDHomework.Core.Discount;

namespace SOLIDHomework.Core
{
    //there are OCP violation
    public class ShoppingCart
    {
        private readonly List<OrderItem> _orderItems;
        private readonly List<IDiscount> _discounts;
        private readonly List<IProductType> _productTypes;

        public ShoppingCart(List<IDiscount> discounts,List<IProductType> productTypes)
        {
            _orderItems = new List<OrderItem>();
            _discounts = discounts;
            _productTypes = productTypes;
        }

        public IEnumerable<OrderItem> OrderItems => _orderItems;

        public void Add(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
        }

        public decimal TotalAmount()
        {
            decimal total = 0;
            foreach (var orderItem in OrderItems)
            {
                decimal productPrice = orderItem.Amount*orderItem.Price; // deafult price
                foreach (var productType in _productTypes)
                {
                    if (productType.DefineType(orderItem))
                    {
                        productPrice = productType.GetPrice(orderItem);
                        break;
                    }
                        
                }
                foreach (var discount in _discounts)
                {
                  discount.ApplyDiscount(orderItem, ref productPrice);
                }
                total += productPrice;
            }

            //calculate tax
            TaxCalculator TaxCalculator = new TaxUS();
            TaxCalculator.ApplyTax(ref total);

            return total;
        }
    }
}