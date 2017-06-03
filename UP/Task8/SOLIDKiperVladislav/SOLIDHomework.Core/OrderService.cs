using System;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework.Core
{
    //Order - check inventory, charge money for credit card and online payments, 
    //tips:
    //think about SRP, DI, OCP
    //maybe for each type of payment type make sense to have own Order-based class?
    public class OrderService
    {
        private readonly InventoryService _inventoryService;
        private readonly UserService _userService;

        private readonly ILogger _logger;

        public OrderService()
        {
            _inventoryService = new InventoryService();
            _userService = new UserService();
            _logger = new MyLogger();
        }

        public void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType, string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer)
        {
            _logger.Write("Start checkout.");
            switch (paymentMethod)
            {
                case PaymentMethod.CreditCard:
                    ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
                    ReserveInventory(shoppingCart);
                    _logger.Write("Success card checkout");
                    break;
                case PaymentMethod.Cash:
                    ReserveInventory(shoppingCart);
                    _logger.Write("Success cash checkout");
                    break;
                case PaymentMethod.OnlineOrder:
                    ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
                    ReserveInventory(shoppingCart);
                    if (notifyCustomer)
                    {
                        NotifyCustomer(username);
                    }
                    _logger.Write("Success online checkout");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null);
            }
        }

        private void NotifyCustomer(string username)
        {
            _logger.Write(string.Format("Email will be sent to {0}", username));
            string customerEmail = _userService.GetByUsername(username).Email;
            if (!String.IsNullOrEmpty(customerEmail))
            {
                try
                {
                    //construct the email message and send it, implementation ignored
                }
                catch (Exception)
                {
                    //log the emailing error, implementation ignored
                }
            }
        }

        private void ReserveInventory(ShoppingCart cart)
        {
            foreach (OrderItem item in cart.OrderItems)
            {
                try
                {
                    _inventoryService.Reserve(item.Code, item.Amount);
                }
                catch (InsufficientInventoryException ex)
                {
                    throw new OrderException("Insufficient inventory for item " + item.Code, ex);
                }
                catch (Exception ex)
                {
                    throw new OrderException("Problem reserving inventory", ex);
                }
            }
        }

        private void ChargeCard(PaymentServiceType paymentServiceType, PaymentDetails paymentDetails, ShoppingCart cart)
        {
            try
            {
                IPayment payment = PaymentFactory.GetPaymentService(paymentServiceType);
                string serviceResponse;
                bool result = payment.Charge(cart.TotalAmount(), new CreditCart
                {
                    CardNumber = paymentDetails.CreditCardNumber, ExpiryDate = paymentDetails.ExpiryDate, NameOnCard = paymentDetails.CardholderName
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

    public class OrderException : Exception
    {
        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class AccountBalanceMismatchException : Exception
    {
    }
}