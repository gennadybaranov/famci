using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Calculators;
using SOLIDHomework.Core.Discounts;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;
using SOLIDHomework.Core.TaxCalculator;

namespace SOLIDHomework
{
    public class Program
    {
        //Entry point to program
        //You don't have to change logic there
        //Tip: that is good place for composition root
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            ConfigureContainer(container);

            var paymentMethod = PaymentMethod.Cash;
            BaseOrderService orderService = OrderServiceFactory.GetOrderService(
                paymentMethod,
                container.Resolve<IInventoryService>(),
                container.Resolve<IUserService>()
            );

            ShoppingCart shoppingCart = container.Resolve<ShoppingCart>();
            shoppingCart.Add(new OrderItem()
                {
                    Amount = 1,
                    SeassonEndDate =  DateTime.Now,
                    Code =  "Test",
                    Price =  10,
                    Type = "Unit"
                });
            orderService.Checkout("TestUser",shoppingCart,new PaymentDetails()
                {
                   CardholderName = "haha",
                   CreditCardNumber =  "41111111111111",
                   ExpiryDate =  DateTime.Now.AddDays(10),
                },true);
        }

        static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<ITaxCalculator, DefaultTaxCalculator>();
            container.RegisterType<ShoppingCart>(new InjectionProperty("TaxCalculator"));
            container.RegisterType<IInventoryService, InventoryService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ICalculator, TotalCalculator>(new InjectionConstructor(
                new List<BaseOrderCalculator>
                    {
                        new SpecialOrderCalculator { Discounts = new List<IDiscount> {new ProductsSetDiscount()} }, 
                        new UnitOrderCalculator { Discounts = new List<IDiscount> {new OldSeasonDiscount()} }, 
                        new WeightOrderCalculator()
                    }));
        }
    }
}
