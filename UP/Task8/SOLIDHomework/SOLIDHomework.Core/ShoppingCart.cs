using System;
using System.Collections.Generic;
using System.Configuration;
using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core
{
    //there are OCP violation
    public class ShoppingCart
    {
        private readonly List<OrderItem> _orderItems;

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

                //Calculate discount 20% for old season items
                if (orderItem.SeasonEndDate <= DateTime.Now)
                {
                    productPrice = productPrice * (1 - 20 / 100M);
                }

                //calculate discount 5% if more than 4 items
                if (orderItem.Amount > 4)
                {
                    productPrice = productPrice*(1 - 5/100M);
                }

                total += productPrice;

                return total;
            }

            //calculate tax
            if (ConfigurationManager.AppSettings["country"] == "US")
            {
                total += total * 0.13M;
            }

            return total;
        }
    }
}