using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public class PayPalPayment : IPayment
    {
        //required for Auth;
        public string AccountName { get; set; }

        public string Password { get; set; }

        public PayPalPayment(string accountName, string pwd)
        {
            this.AccountName = accountName;
            this.Password = pwd;
            this.PayPalWebService = new PayPalWebService();
        }

        public IPayPalWebService PayPalWebService { get; set; }

        public bool Charge(decimal amount, CreditCart creditCart, out string serviceResponse)
        {
            bool result = true;
            string token = PayPalWebService.GetTransactionToken(AccountName, Password);
            serviceResponse = PayPalWebService.Charge(amount, token, creditCart);
            if (!serviceResponse.Contains("200OK") && !serviceResponse.Contains("Success"))
            {
                result = false;
            }
            return result;
        }
    }
}