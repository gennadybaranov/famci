using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework.Core.Orders
{
    public class OnlineOrder : BaseCardOrder
    {
        private readonly IUserService _userService;

        public OnlineOrder(IInventoryService inventoryService, ILogger logger, IUserService userService) : base(inventoryService, logger)
        {
            _userService = userService;
        }

        public override void Checkout(PaymentMethod paymentMethod, PaymentServiceType paymentServiceType, string username, ShoppingCart shoppingCart, PaymentDetails paymentDetails, bool notifyCustomer)
        {
            ChargeCard(paymentServiceType, paymentDetails, shoppingCart);
            ReserveInventory(shoppingCart);
            if (notifyCustomer)
            {
                NotifyCustomer(username);
            }
            _logger.Write("Success online checkout");
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
    }
}
