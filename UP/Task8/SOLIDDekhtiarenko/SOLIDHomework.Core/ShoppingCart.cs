using System.Collections.Generic;
using SOLIDHomework.Core.Calculators;
using SOLIDHomework.Core.TaxCalculator;
using Ninject;
using SOLIDHomework.Core.Discounts;

namespace SOLIDHomework.Core
{
    //there are OCP and SOC violation
    //
    public class ShoppingCart
    {
        private readonly List<OrderItem> orderItems;
        [Inject]
        public IDiscount[] Discounts { get; set; }
        
        [Inject]
        public ITaxCalculator TaxCalculator { get; set; }

        public ShoppingCart()
        {
            orderItems = new List<OrderItem>();
        }

        public IEnumerable<OrderItem> OrderItems => orderItems;

        public void Add(OrderItem orderItem)
        {
            orderItems.Add(orderItem);
        }

        public decimal TotalAmount()
        {
            var total = 0M;
            foreach (OrderItem orderItem in orderItems)
            {
                var ItemPrice = 0M;
                if (orderItem.Type == "Weight")
                {
                    ItemPrice = orderItem.Amount * orderItem.Price / 1000M;
                }
                else
                {
                    ItemPrice = orderItem.Amount * orderItem.Price;
                }


                foreach (var discount in Discounts)
                {
                    if (discount.Approached(orderItem))
                    {
                        ItemPrice = discount.CalculateDiscountedPrice(ItemPrice);
                    }
                }
                total += ItemPrice;
            }

            TaxCalculator = new TaxCalculatorUS();
            total += TaxCalculator.CalculateTax(total);

            return total;
        }
    }
}