using System;
using System.Configuration;

namespace SOLIDHomework.Core.Discounts
{
    public class OldSeasonDiscount : IDiscount
    {
        private readonly decimal discountAmnt;

        public bool Approached(OrderItem orderItem)
        {
            return orderItem.Amount > 3;
        }

        public decimal CalculateDiscountedPrice(decimal total)
        {
            return total * 0.6M;
        }
        public bool Discountable(OrderItem item)
        {
            return item.Amount > 3;
        }

        public OldSeasonDiscount()
        {
            discountAmnt = 0;
            decimal.TryParse(ConfigurationManager.AppSettings["oldSeasonDiscount"], out discountAmnt);
        }
        
    }
}