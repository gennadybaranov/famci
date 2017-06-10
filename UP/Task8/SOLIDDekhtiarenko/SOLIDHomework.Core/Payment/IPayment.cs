namespace SOLIDHomework.Core.Payment
{
    public interface IPayment
    {
        bool Charge(decimal amount, CreditCart creditCart, out string serviceResponse);
    }
}