using Ninject.Modules;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Discounts;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Orders;
using SOLIDHomework.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDHomework
{
    class Inject : NinjectModule
    {
        public override void Load()
        {
            Bind<IInventoryService>().To<InventoryService>();
            Bind<ShoppingCartFactory>().ToSelf();
            Bind<IDiscount>().To<AmountDiscount>();
            Bind<IDiscount>().To<OldSeasonDiscount>();
            Bind<IUserService>().To<UserService>();
            Bind<ILogger>().To<Logger>();
            Bind<ITaxCalculator>().To<USTaxCalculator>();
            Bind<Order>().To<CashOrder>().Named(PaymentMethod.Cash.ToString());
            Bind<Order>().To<CardOrder>().Named(PaymentMethod.CreditCard.ToString());
            Bind<Order>().To<OnlineOrder>().Named(PaymentMethod.OnlineOrder.ToString());
        }
    }
}
