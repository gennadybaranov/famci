using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public class WorldPayWebService : IWorldPayService
    {
        public string Charge(decimal amount, CreditCard creditCart, string bankID, string domenID)
        {
            return "Success";
        }
    }
}