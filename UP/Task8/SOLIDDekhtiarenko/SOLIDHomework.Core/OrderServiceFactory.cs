using System;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework.Core
{
    public class OrderServiceFactory
    {
        public static BaseOrderService GetOrderService(PaymentMethod paymentMethod, IInventoryService inventoryService,
            IUserService userService)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    return new CashOrderService(inventoryService, userService);

                case PaymentMethod.CreditCard:
                    return new CardOrderService(inventoryService, userService);

                case PaymentMethod.OnlineOrder:
                    return new OnlineOrderService(inventoryService, userService);

                default:
                    throw new NotImplementedException("No such service.");
            }
        }
    }
}