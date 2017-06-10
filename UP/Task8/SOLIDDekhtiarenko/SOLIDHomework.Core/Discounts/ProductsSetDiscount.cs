using System.Configuration;

namespace SOLIDHomework.Core.Discounts
{
    public class ProductsSetDiscount : IDiscount
    {
        private readonly int setAmnt;

        public decimal CalculateDiscountedPrice(decimal total)
        {
            return total * 0.8M;
        }

        public bool Approached(OrderItem item)
        {
            return item.Amount > 3;
        }

        public ProductsSetDiscount()
        {
            setAmnt = int.MaxValue;
            int.TryParse(ConfigurationManager.AppSettings["setAmount"], out setAmnt);
        }

        public decimal CalculateDiscount(OrderItem orderItem, decimal total)
        {
            var setsOfProducts = orderItem.Amount / setAmnt;
            total -= setsOfProducts * orderItem.Price;
            return total;
        }
    }
}