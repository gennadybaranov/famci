using System;
using System.Configuration;

namespace SOLIDHomework.Core.Payment
{
    public class PaymentFactory
    {
        public static IPayment GetPaymentService(PaymentServiceType serviceType)
        {
            switch (serviceType)
            {
                case PaymentServiceType.PayPal:
                    var accountName = ConfigurationManager.AppSettings["accountName"];
                    var password = ConfigurationManager.AppSettings["password"];
                    return new PayPalPayment(accountName, password);

                case PaymentServiceType.WorldPay:
                    var bankID = ConfigurationManager.AppSettings["BankID"];
                    var domenId = ConfigurationManager.AppSettings["DomenID"];
                    return new WorldPayPayment(bankID, domenId);
                default:
                    throw new NotImplementedException("No such service.");
            }
        }
    }
}