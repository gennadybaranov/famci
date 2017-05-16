using System;

namespace SOLIDHomework.Core.Model
{
    public class OrderItem
    {
        public int Amount { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public DateTime SeasonEndDate { get; set; }

        public string Code { get; set; }
    }
}