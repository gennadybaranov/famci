using System.Configuration;

namespace SOLIDHomework.Core.Discounts
{
    public class ProductsSetDiscount: IDiscount
    {
        private readonly int setAmnt;

        public ProductsSetDiscount()
        {
            setAmnt = int.MaxValue;
            int.TryParse(ConfigurationManager.AppSettings["setAmount"], out setAmnt);
        }

        public decimal CalculateDiscount(OrderItem orderItem, decimal total)
        {
            int setsOfProducts = orderItem.Amount / setAmnt;
            total -= setsOfProducts * orderItem.Price;
            return total;
        }
    }
}
