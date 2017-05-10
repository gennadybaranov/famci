using System;
using SOLIDHomework.Core;
using SOLIDHomework.Core.Model;
using SOLIDHomework.Core.Payment;

namespace SOLIDHomework
{
    public class Program
    {
        //Entry point to program
        //Tip: that is good place for composition root
        static void Main(string[] args)
        {
            //Checkout by cash
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.Add(new OrderItem
            {
                Amount = 1,
                SeasonEndDate = DateTime.Now,
                Code = "Test",
                Price = 10,
                Type = "Unit"
            });

            var orderService = new OrderService();
            orderService.Checkout(PaymentMethod.Cash, PaymentServiceType.PayPal, "TestUser", shoppingCart, null, false);

            //Checkout by credit card
            shoppingCart = new ShoppingCart();
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
            shoppingCart = new ShoppingCart();
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
        }
    }
}