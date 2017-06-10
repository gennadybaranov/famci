using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework.Core
{
    public abstract class Order
    {
        protected ILogger _logger;

        private readonly IInventoryService _inventoryService;
        protected Order(IInventoryService inventoryService, ILogger logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        protected void ReserveInventory(ShoppingCart cart)
        {
            foreach (var item in cart.OrderItems)
            {
                try
                {
                    _inventoryService.Reserve(item.Code, item.Amount);
                }
                catch (InsufficientInventoryException ex)
                {
                    throw new OrderService.OrderException("Insufficient inventory for item " + item.Code, ex);
                }
                catch (Exception ex)
                {
                    throw new OrderService.OrderException("Problem with reserving inventory", ex);
                }
            }
        }

        public abstract void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType,
            string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer);
    }
}
