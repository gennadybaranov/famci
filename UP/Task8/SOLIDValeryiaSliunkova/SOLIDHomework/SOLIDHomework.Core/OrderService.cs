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

    public class OrderException : Exception
    {
        public OrderException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AccountBalanceMismatchException : Exception { }


    public interface ICashOrderService
    {
        void Checkout(ShoppingCart shoppingCart);
    }

    public interface ICreditCardOrderService
    {
        void Checkout(ShoppingCart shoppingCart, PaymentServiceType paymentServiceType, PaymentDetails paymentDetails);
    }

    public interface IOnlineOrderService
    {
        void Checkout(ShoppingCart shoppingCart, PaymentServiceType paymentServiceType, PaymentDetails paymentDetails);
    }


    public interface IChargeCardService
    {
        void ChargeCard(PaymentServiceType paymentServiceType, PaymentDetails paymentDetails, ShoppingCart shoppingCart);
    }

    public interface IReserveInventoryService
    {
        void ReserveInventory(ShoppingCart cart);
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

    public class ChargeCardService : IChargeCardService
    {
        private IChargeCardService _chargeCardService;

        public ChargeCardService(IChargeCardService chargeCardService)
        {
            _chargeCardService = chargeCardService;
        }

        public void ChargeCard(PaymentServiceType paymentServiceType, PaymentDetails paymentDetails, ShoppingCart shoppingCart)
        {
            try
            {
                IPayment payment = PaymentFactory.GetPaymentService(paymentServiceType);
                string serviceResponse;
                bool result = payment.Charge(shoppingCart.TotalAmount(), new CreditCart
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


    public class CreditCardOrderService : ICreditCardOrderService
    {
        private IReserveInventoryService _reserveInventoryService;
        private IChargeCardService _chargeCardService;

        public CreditCardOrderService(IReserveInventoryService reserveInventoryService, IChargeCardService chargeCardService)
        {
            _reserveInventoryService = reserveInventoryService;
            _chargeCardService = chargeCardService;
        }


        public void Checkout(ShoppingCart shoppingCart, PaymentServiceType paymentServiceType, PaymentDetails paymentDetails)
        {
            _reserveInventoryService.ReserveInventory(shoppingCart);
            _chargeCardService.ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
            LoggerContext.Current.Write("Success cash checkout");
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
            _reserveInventoryService.ReserveInventory(shoppingCart);
            LoggerContext.Current.Write("Success cash checkout");
        }
    }

    public class OnlineOrderService : IOnlineOrderService
    {
        private IReserveInventoryService _reserveInventoryService;
        private IChargeCardService _chargeCardService;

        public OnlineOrderService(IReserveInventoryService reserveInventoryService, IChargeCardService chargeCardService)
        {
            _reserveInventoryService = reserveInventoryService;
            _chargeCardService = chargeCardService;
        }


        public void Checkout(ShoppingCart shoppingCart, PaymentServiceType paymentServiceType, PaymentDetails paymentDetails)
        {
            _reserveInventoryService.ReserveInventory(shoppingCart);
            _chargeCardService.ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
            LoggerContext.Current.Write("Success cash checkout");
        }
    }
}