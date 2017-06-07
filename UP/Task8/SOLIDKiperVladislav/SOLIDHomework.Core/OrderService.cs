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
    //maybe for each type of payment  make sense to have own Order-based class?

    public interface INotifyCustomer
    {
        void NotifyCustomer(string username);
    }

    public class NotifyCustomer : INotifyCustomer
    {
        private UserService _userService;

        public NotifyCustomer(UserService userService)
        {
            _userService = userService;
        }


        void INotifyCustomer.NotifyCustomer(string username)
        {
            LoggerContext.Current.Write(string.Format("Email will be sent to {0}", username));
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
    }

    public interface ICashOrderService
    {
        void Checkout(ShoppingCart cart);
    }

    public interface ICreditCartService
    {
        void Checkout(PaymentServiceType paymentServiceType, ShoppingCart cart, PaymentDetails PaymentDetails);
    }

    public interface IOnlineOrderService
    {
        void Checkout(PaymentServiceType paymentServiceType,ShoppingCart cart, PaymentDetails PaymentDetails,
            string username, bool notifyCustomer);
    }

    public class CreditCartService : ICreditCartService
    {

        private IReserveInventoryService _reserveInventoryService;
        private IChargeCardService _chargeCardService;

        public CreditCartService(IReserveInventoryService reserveInventoryService, IChargeCardService chargeCardService)
        {
            _reserveInventoryService = reserveInventoryService;
            _chargeCardService = chargeCardService;
        }

        public void Checkout(PaymentServiceType paymentServiceType, ShoppingCart cart, PaymentDetails PaymentDetails)
        {
            LoggerContext.Current.Write("Start checkout");
            _reserveInventoryService.ReserveInventory(cart);
            _chargeCardService.ChargeCard(paymentServiceType, cart, PaymentDetails);
            LoggerContext.Current.Write("Success credit cart checkout");
        }
    }

    public class OnlineOrderService : IOnlineOrderService
    {

        private IReserveInventoryService _reserveInventoryService;
        private IChargeCardService _chargeCardService;
        private INotifyCustomer _notifyCustomer;

        public OnlineOrderService(IReserveInventoryService reserveInventoryService, IChargeCardService chargeCardService,
            INotifyCustomer notifyCustomer )
        {
            _reserveInventoryService = reserveInventoryService;
            _chargeCardService = chargeCardService;
            _notifyCustomer = notifyCustomer;
        }

        public void Checkout(PaymentServiceType paymentServiceType, ShoppingCart cart, PaymentDetails PaymentDetails,
            string username, bool notifyCustomer)
        {
            LoggerContext.Current.Write("Start checkout");
            _reserveInventoryService.ReserveInventory(cart);
            _chargeCardService.ChargeCard(paymentServiceType, cart, PaymentDetails);
            if (notifyCustomer)
            {
                _notifyCustomer.NotifyCustomer(username);
            }
            LoggerContext.Current.Write("Success online checkout");
        }
    }

    public interface IReserveInventoryService
    {
        void ReserveInventory(ShoppingCart cart);
    }

    public interface IChargeCardService
    {
        void ChargeCard(PaymentServiceType paymentServiceType, ShoppingCart cart, PaymentDetails PaymentDetails);
    }

    public class ChargeCardService : IChargeCardService
    {
        public void ChargeCard(PaymentServiceType paymentServiceType, ShoppingCart cart, PaymentDetails paymentDetails)
        {
            try
            {
                IPayment payment = PaymentFactory.GetPaymentService(paymentServiceType);
                string serviceResponse;
                bool result = payment.Charge(cart.TotalAmount(), new CreditCart()
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
    
    public class ReserveInventoryService : IReserveInventoryService
    {
        private IInventoryService _inventoryService;


        public ReserveInventoryService(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void ReserveInventory(ShoppingCart cart)
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
    }

    public class CashOrderService : ICashOrderService
    {
        private IReserveInventoryService _reserveInventoryService;


        public CashOrderService(IReserveInventoryService reserveInventoryService)
        {
            _reserveInventoryService = reserveInventoryService;
        }

        public void Checkout(ShoppingCart shoppingCart)
        {
            LoggerContext.Current.Write("Start checkout");
            _reserveInventoryService.ReserveInventory(shoppingCart);
            LoggerContext.Current.Write("Success cash checkout");
        }
    }

    public static class LoggerContext
    {
        private static ILogger _current;
        private static ILogger _defaultLogger = new MyLogger();

        public static ILogger Current
        {
            get
            {
                if (_current == null)
                {
                    _current = _defaultLogger;
                }

                return _current;
            }
            set
            {
                _current = value;
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