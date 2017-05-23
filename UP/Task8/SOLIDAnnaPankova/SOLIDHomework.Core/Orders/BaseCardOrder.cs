using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Services;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Loggers;

namespace SOLIDHomework.Core.Orders
{
    public abstract class BaseCardOrder : Order
    {
        public BaseCardOrder(IInventoryService inventoryService, ILogger logger) : base(inventoryService, logger)
        {
        }

        protected void ChargeCard(PaymentServiceType paymentServiceType, PaymentDetails paymentDetails, ShoppingCart cart)
        {
            try
            {
                IPayment payment = PaymentFactory.GetPaymentService(paymentServiceType);
                string serviceResponse;
                bool result = payment.Charge(cart.TotalAmount(), new CreditCard
                {
                    CardNumber = paymentDetails.CreditCardNumber,
                    ExpiryDate = paymentDetails.ExpiryDate,
                    NameOnCard = paymentDetails.CardholderName
                }, out serviceResponse);

                if (!result)
                {
                    throw new Exception(String.Format("Error on charge : {0}", serviceResponse));
                }
            }
            catch (AccountBalanceMismatchException ex)
            {
                throw new OrderException("The card gateway rejected the card based on the address provided.", ex);
            }
            catch (Exception ex)
            {
                throw new OrderException("There was a problem with your card.", ex);
            }
        }
    }
}
