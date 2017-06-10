using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Discounts;
using SOLIDHomework.Core.Loggers;
using SOLIDHomework.Core.Model;
//using SOLIDHomework.Core.Orders;
using SOLIDHomework.Core.Services;
using SOLIDHomework.Core.TaxCalculator;

namespace SOLIDHomework
{
    class Ninject : NinjectModule
    {
        public override void Load()
        {
            Bind<IInventoryService>().To<InventoryService>();
            Bind<IDiscount>().To<OldSeasonDiscount>();
            Bind<IDiscount>().To<ProductsSetDiscount>();
            Bind<ILogger>().To<MyLogger>();
            Bind<IUserService>().To<UserService>();
            Bind<ITaxCalculator>().To<TaxCalculatorUS>();
        }
    }
}
