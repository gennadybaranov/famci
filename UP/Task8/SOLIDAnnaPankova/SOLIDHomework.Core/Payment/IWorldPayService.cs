using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public interface IWorldPayService
    {
        string Charge(decimal amount, CreditCard creditCart, string bankID, string domenID);
    }
}