using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core
{
    public interface IDiscount
    {
        bool AppliesTo(OrderItem item);
        decimal RecalculateItemPrice(decimal price);
    }
}