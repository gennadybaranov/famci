namespace SOLIDHomework.Core.Payment
{
    public class PayPalPayment : IPayment
    {
        public PayPalPayment(string accountName, string pwd)
        {
            AccountName = accountName;
            Password = pwd;
            PayPalWebService = new PayPalWebService();
        }

        //required for Auth;
        public string AccountName { get; set; }

        public string Password { get; set; }

        public IPayPalWebService PayPalWebService { get; set; }

        public bool Charge(decimal amount, CreditCart creditCart, out string serviceResponse)
        {
            var result = true;
            var token = PayPalWebService.GetTransactionToken(AccountName, Password);
            serviceResponse = PayPalWebService.Charge(amount, token, creditCart);
            if (!serviceResponse.Contains("200OK") && !serviceResponse.Contains("Success"))
                result = false;
            return result;
        }
    }
}