using SOLIDHomework.Core.Model;

namespace SOLIDHomework.Core.Payment
{
    public class WorldPayPayment : IPayment
    {
        //required for Auth;
        public string BankID { get; set; }
        public string DomenID { get; set; }

        public WorldPayPayment(string bankId, string domenId)
        {
            this.BankID = bankId;
            this.DomenID = domenId;
        }

        public IWorldPayService WorldPayWebService { get; set; }

        public bool Charge(decimal amount, CreditCart creditCart, out string serviceResponse)
        {
            bool result = true;
            serviceResponse = WorldPayWebService.Charge(amount, creditCart, BankID, DomenID);
            if (!serviceResponse.Contains("200OK") && !serviceResponse.Contains("Success"))
            {
                result = false;
            }
            return result;
        }
    }
}