using System;
using System.Configuration;
using Ninject;
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
        private readonly IInventoryService _inventoryService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IKernel _kernel;

        protected OrderService(IInventoryService inventoryService, IUserService userService)
        {
            _inventoryService = inventoryService;
            _userService = userService;
        }

        public void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType, string username,
            ShoppingCart shoppingCart, PaymentDetails paymentDetails,
            bool notifyCustomer)
        {
            _logger.Write("Start checkout.");
            var order = _kernel.Get<Order>(paymentMethod.ToString());
            if (order == null)
            {
                throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null);
            }
            order.Checkout(paymentMethod, paymentServiceType, username, shoppingCart, paymentDetails, notifyCustomer);
        }

        public class OrderException : Exception
        {
            public OrderException(string message, Exception exception) : base(message, exception)
            {
                //if (inventoryService == null)
                //{
                //    throw new ArgumentException("Inventory Service");
                //}

                //foreach (OrderItem item in cart.OrderItems)
                //{
                //    try
                //    {
                //        _inventoryService.Reserve(item.Code, item.Amount);
                //    }
                //    catch (InsufficientInventoryException ex)
                //    {
                //        throw new OrderException("Insufficient inventory for item " + item.Code, ex);
                //    }

                //}
            }
        }

        public class AccountException : Exception
        {
            public AccountException(string message, Exception exception) : base(message, exception)
            {
                //if (userService == null)
                //{
                //    throw new ArgumentException("User Service");
                //}

                //foreach (OrderItem item in cart.OrderItems)
                //{
                //    try
                //    {
                //        _inventoryService.Reserve(item.Code, item.Amount);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new OrderException("Problem reserving inventory", ex);
                //    }
                //}

            }
        }

        public void NotifyCustomer(string username)
        {
            string customerEmail = _userService.GetByUsername(username).Email;
            if (!String.IsNullOrEmpty(customerEmail))
            {
                try
                {
                    //construct the email message and send it, implementation ignored
                }
                catch (Exception ex)
                {
                    //log the emailing error, implementation ignored
                }
            }
        }
        public class AccountBalanceMismatchException : Exception
        {
        }
    }
}
