using System;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;
using SOLIDHomework.Core.Services;
using Microsoft.Practices.Unity;
using SOLIDHomework.Core.Discount;
using SOLIDHomework.Core.ProductType;
using System.Collections.Generic;

namespace SOLIDHomework
{
    public class Program
    {
        //Entry point to program
        //Tip: that is good place for composition root
        static void Main(string[] args)
        {
            var Container = new UnityContainer();
            Container.RegisterType<IInventoryService, TextBasedInventoryService>();
            Container.RegisterType<IReserveInventoryService, ReserveInventoryService>();
            Container.RegisterType<IChargeCardService, ChargeCardService>();
            Container.RegisterType<ICashOrderService, CashOrderService>();

            var cashService = Container.Resolve<ICashOrderService>();



            //Checkout by cash
            var SeasonDiscount = new SeasonEndDiscount();
            var UnitProduct = new UnitType();
            var WeightProduct = new WeightType();
            List<IDiscount> discounts = new List<IDiscount>();
            discounts.Add(SeasonDiscount);
            List<IProductType> productTypes = new List<IProductType>();
            productTypes.Add(WeightProduct);
            productTypes.Add(UnitProduct);
            ShoppingCart shoppingCart = new ShoppingCart(discounts,productTypes);
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });

            var inventoryService = new InventoryService();
            var reserveInventoryService = new ReserveInventoryService(inventoryService);
            var CashService = new CashOrderService(reserveInventoryService);
            CashService.Checkout(shoppingCart);

            //Checkout by credit card
            shoppingCart = new ShoppingCart(discounts,productTypes);
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });
            var chargeCard = new ChargeCardService();
            var CreditCartService = new CreditCartService(reserveInventoryService, chargeCard);
            CreditCartService.Checkout(PaymentServiceType.PayPal, shoppingCart, new PaymentDetails()
            {
                CardholderName = "haha",
                CreditCardNumber = "41111111111111",
                ExpiryDate = DateTime.Now.AddDays(10),
            });
        
            //Checkout online
            shoppingCart = new ShoppingCart(discounts,productTypes);
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });
            var userService = new UserService();
            var notifyCustomer = new NotifyCustomer(userService);
            var onlineOrder = new OnlineOrderService(reserveInventoryService, chargeCard, notifyCustomer);
            onlineOrder.Checkout(PaymentServiceType.PayPal, shoppingCart, new PaymentDetails()
            {
                CardholderName = "haha",
                CreditCardNumber = "41111111111111",
                ExpiryDate = DateTime.Now.AddDays(10),
            }, "TestUser", true);
        }
    }
}