using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public class PayPalWebService : IPayPalWebService
    {
        //web based service
        public string GetTransactionToken(string accountName, string password)
        {
            return "Something";
        }

        public string Charge(decimal amount, string token, CreditCard creditCart)
        {
            return "200OK";
        }
    }
}