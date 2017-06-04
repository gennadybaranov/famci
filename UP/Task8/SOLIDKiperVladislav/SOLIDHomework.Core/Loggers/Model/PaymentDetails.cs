using System;

namespace SOLIDHomework.Core.Model
{
    public class PaymentDetails
    {
        public string CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardholderName { get; set; }
    }
}