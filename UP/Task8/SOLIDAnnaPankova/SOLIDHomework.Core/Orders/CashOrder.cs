using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework.Core.Orders
{
    public class CashOrder : Order
    { 
        public CashOrder(ILogger logger, IInventoryService inventoryService) : base(inventoryService, logger)
        {
        }

        public override void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType,
            string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer)
        {
            ReserveInventory(shoppingCart);
            _logger.Write("Success cash checkout");
        }
    }
}
