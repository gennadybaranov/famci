using System;
using System.Collections.Generic;
using System.Configuration;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Calculator;

namespace SOLIDHomework.Core
{
    //there are OCP violation
    public class ShoppingCart
    {
        public ITaxCalculator TaxCalculator { get; set; }
        private readonly List<OrderItem> orderItems;
        private readonly ICalculator calculator;

        public ShoppingCart(ICalculator calculator)
        {
            this.orderItems = new List<OrderItem>();
            this.calculator = calculator;
            this.TaxCalculator = new TaxCalculator();
        }

        public IEnumerable<OrderItem> OrderItems => orderItems;

        public void Add(OrderItem orderItem)
        {
            orderItems.Add(orderItem);
        }

        public decimal TotalAmount()
        {
            decimal total = calculator.CalculateTotal(this);
            total = TaxCalculator.CalculateTax(total);
            return total;
        }
    }
}