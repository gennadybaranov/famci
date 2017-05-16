using System;
using System.Collections.Generic;
using System.Configuration;
using SOLIDHomework.Core.Model;
using Ninject;

namespace SOLIDHomework.Core
{
    //there are OCP violation
    public class ShoppingCart
    {
        private readonly List<OrderItem> _orderItems;

        [Inject]
        public IDiscount[] discounts { private get; set; }

        [Inject]
        public ITaxCalculator TaxCalculator { private get; set; }

        public ShoppingCart()
        {
            _orderItems = new List<OrderItem>();
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
                decimal productPrice;
                if (orderItem.Type == "Unit")
                {
                    productPrice = orderItem.Price;
                }
                else
                {
                    productPrice = orderItem.Amount*orderItem.Price;
                }

                foreach(var discount in discounts)
                {
                    if (discount.AppliesTo(orderItem)) {
                        productPrice = discount.RecalculateItemPrice(productPrice);
                    }
                }
                total += productPrice;
                
            }


            //calculate tax

            if (TaxCalculator == null)
            {
                TaxCalculator = new USTaxCalculator();
            }
            total += TaxCalculator.GetTax(total);

            return total;
        }
    }
}