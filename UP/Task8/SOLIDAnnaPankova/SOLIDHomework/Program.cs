using System;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Discounts;
using SOLIDHomework.Core.Payment;
using Ninject;
using SOLIDHomework.Core.Services;

namespace SOLIDHomework
{
    public class Program
    {
        //Entry point to program
        //Tip: that is good place for composition root
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new Inject());
            var factory = kernel.Get<ShoppingCartFactory>();
            //Checkout by cash
            ShoppingCart shoppingCart = factory.Create();
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });

            var orderService = kernel.Get<OrderService>();
            orderService.Checkout(PaymentMethod.Cash, PaymentServiceType.PayPal, "TestUser", shoppingCart, null, false);

            //Checkout by credit card
            shoppingCart = factory.Create();
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });
            orderService.Checkout(PaymentMethod.CreditCard, PaymentServiceType.WorldPay, "TestUser", shoppingCart, new PaymentDetails()
            {
                CardholderName = "haha",
                CreditCardNumber = "41111111111111",
                ExpiryDate = DateTime.Now.AddDays(10),
            }, false);


            //Checkout online
            shoppingCart = factory.Create();
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });
            orderService.Checkout(PaymentMethod.OnlineOrder, PaymentServiceType.PayPal, "TestUser", shoppingCart, new PaymentDetails()
            {
                CardholderName = "haha",
                CreditCardNumber = "41111111111111",
                ExpiryDate = DateTime.Now.AddDays(10),
            }, true);
            Console.ReadLine();
        }
        
    }

}