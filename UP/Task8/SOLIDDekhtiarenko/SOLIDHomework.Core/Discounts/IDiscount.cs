namespace SOLIDHomework.Core.Discounts
{
    public interface IDiscount
    {
        decimal CalculateDiscountedPrice(decimal total);

        bool Approached(OrderItem item);
    }
}