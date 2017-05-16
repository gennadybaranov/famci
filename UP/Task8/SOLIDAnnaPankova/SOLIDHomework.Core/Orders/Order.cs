using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework.Core.Orders
{
    public abstract class Order
    {
        protected ILogger _logger;

        private IInventoryService _inventoryService;
        public Order(IInventoryService inventoryService, ILogger logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }
        protected void ReserveInventory(ShoppingCart cart)
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

        public abstract void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType,
            string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer);
    }

}
