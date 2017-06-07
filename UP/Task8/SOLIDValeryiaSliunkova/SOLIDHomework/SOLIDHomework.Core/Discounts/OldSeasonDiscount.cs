using System;
using System.Configuration;

namespace SOLIDHomework.Core.Discounts
{
    public class OldSeasonDiscount : IDiscount
    {
        private readonly decimal discountAmnt;

        public OldSeasonDiscount()
        {
            discountAmnt = 0;
            decimal.TryParse(ConfigurationManager.AppSettings["oldSeasonDiscount"], out discountAmnt);
        }

        public decimal CalculateDiscount(OrderItem orderItem, decimal total)
        {
            if (orderItem.SeassonEndDate <= DateTime.Now)
            {
                total = total * (1 - discountAmnt / 100M);
            }
            return total;
        }
    }
}
