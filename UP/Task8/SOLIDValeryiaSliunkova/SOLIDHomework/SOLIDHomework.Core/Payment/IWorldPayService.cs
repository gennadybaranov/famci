using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public interface IWorldPayService
    {
        string Charge(decimal amount, CreditCart creditCart, string bankID, string domenID);
    }
}