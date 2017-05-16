using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;
using SOLIDHomework.Core.Loggers;

namespace SOLIDHomework.Core.Orders
{
    public class CardOrder : BaseCardOrder
    {
        public CardOrder(ILogger logger, IInventoryService inventoryService) : base(inventoryService, logger)
        {
        }

        public override void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType, string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer)
        {
            ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
            ReserveInventory(shoppingCart);
            _logger.Write("Success card checkout");
        }
    }
}
