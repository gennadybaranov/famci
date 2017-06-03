namespace SOLIDHomework.Core.Discounts
{
    public interface IDiscount
    {
        decimal CalculateDiscount(OrderItem orderItem, decimal total);
    }
}
