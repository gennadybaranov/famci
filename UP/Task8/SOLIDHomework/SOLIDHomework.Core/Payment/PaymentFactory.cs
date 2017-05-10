using System;
using System.Configuration;

namespace SOLIDHomework.Core.Payment
{
    public static class PaymentFactory
    {
        public static IPayment GetPaymentService(PaymentServiceType serviceType)
        {
            switch (serviceType)
            {
                case PaymentServiceType.PayPal:
                    string accountName = ConfigurationManager.AppSettings["accountName"];
                    string password = ConfigurationManager.AppSettings["password"];
                    return new PayPalPayment(accountName, password);

                case PaymentServiceType.WorldPay:
                    string bankId = ConfigurationManager.AppSettings["BankID"];
                    string domenId = ConfigurationManager.AppSettings["DomenID"];
                    return new WorldPayPayment(bankId, domenId);
                default:
                    throw new NotImplementedException("No such service.");
            }
        }
    }
}