namespace SOLIDHomework.Core.Payment
{
    public class WorldPayWebService : IWorldPayService
    {
        public string Charge(decimal amount, CreditCart creditCart, string bankID, string domenID)
        {
            return "Success";
        }
    }
}