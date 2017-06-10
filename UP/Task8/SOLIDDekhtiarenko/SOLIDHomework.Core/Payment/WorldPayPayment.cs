namespace SOLIDHomework.Core.Payment
{
    public class WorldPayPayment : IPayment
    {
        public WorldPayPayment(string bankId, string domenId)
        {
            BankID = bankId;
            DomenID = domenId;
        }

        //required for Auth;
        public string BankID { get; set; }

        public string DomenID { get; set; }

        public IWorldPayService WorldPayWebService { get; set; }

        public bool Charge(decimal amount, CreditCart creditCart, out string serviceResponse)
        {
            var result = true;
            serviceResponse = WorldPayWebService.Charge(amount, creditCart, BankID, DomenID);
            if (!serviceResponse.Contains("200OK") && !serviceResponse.Contains("Success"))
                result = false;
            return result;
        }
    }
}