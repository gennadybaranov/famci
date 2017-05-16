using System;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;
using Ninject;
using SOLIDHomework.Core.Orders;

namespace SOLIDHomework.Core
{
    //Order - check inventory, charge money for credit card and online payments, 
    //tips:
    //think about SRP, DI, OCP
    //maybe for each type of payment type make sense to have own Order-based class?
    public class OrderService
    {
        private readonly ILogger _logger;
        private readonly IKernel _kernel;

        public OrderService(ILogger logger, IKernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
        }

        public void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType,
            string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer)
        {
            _logger.Write("Start checkout.");
            var order = _kernel.Get<Order>(paymentMethod.ToString());
            if (order == null)
            {
                throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null);
            }
            order.Checkout(paymentMethod, paymentServiceType, username, shoppingCart, paymentDetails, notifyCustomer);
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