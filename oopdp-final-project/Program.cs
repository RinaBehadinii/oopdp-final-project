using Microsoft.EntityFrameworkCore;
using oopdp_final_project.DatabaseConnection;
using oopdp_final_project.Entities;
using oopdp_final_project.Models;
using oopdp_final_project.Observers;
using oopdp_final_project.Services.PaymentProcessing;
using oopdp_final_project.Services.PaymentProcessing.PayPalProcessing;
using oopdp_final_project.Services.PaymentProcessing.VisaProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Food Delivery Service!");

        Console.WriteLine("Please enter your name to place the order:");
        string userName = Console.ReadLine();

        using (var context = DatabaseManager.Instance)
        {
            Console.WriteLine("Select a restaurant by number:");
            var restaurants = context.Restaurants.Include(r => r.MenuItems).ToList();
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurants[i].Name}");
            }

            int restaurantChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            Restaurant selectedRestaurant = restaurants[restaurantChoice];

            Console.WriteLine($"Menu for {selectedRestaurant.Name}:");
            foreach (var item in selectedRestaurant.MenuItems)
            {
                Console.WriteLine($"{item.ItemId}. {item.Name} - ${item.Price}");
            }

            Console.WriteLine("Enter the item numbers you want to order (comma separated):");
            var itemNumbers = Console.ReadLine().Split(',').Select(int.Parse).ToList();
            List<MenuItem> orderedItems = selectedRestaurant.MenuItems
                .Where(item => itemNumbers.Contains(item.ItemId))
                .ToList();

            decimal orderAmount = orderedItems.Sum(item => item.Price);

            Console.WriteLine("\nOrder Summary:");
            orderedItems.ForEach(item => Console.WriteLine($"{item.Name} - ${item.Price}"));
            Console.WriteLine($"Total Amount: ${orderAmount}");
            Console.WriteLine("Do you want to proceed with the order? (yes/no)");
            string confirmation = Console.ReadLine().ToLower();
            if (confirmation != "yes")
            {
                Console.WriteLine("Order canceled.");
                return;
            }

            Console.WriteLine("Choose a payment method (paypal/visa):");
            string paymentMethod = Console.ReadLine().ToLower();
            IPaymentProcessor paymentProcessor = GetPaymentProcessor(paymentMethod);
            if (paymentProcessor == null)
            {
                Console.WriteLine("Invalid payment method selected. Exiting.");
                return;
            }

            bool paymentSuccess = paymentProcessor.ProcessPayment(orderAmount, "USD");

            Order order = new Order
            {
                CustomerName = userName,
                CustomerAddress = "123 Main St",
                TotalPrice = orderAmount,
                Status = paymentSuccess ? "Paid" : "Payment Failed",
                CreatedAt = DateTime.Now
            };
            order.Attach(new RestaurantNotification());

            if (paymentSuccess)
            {
                Console.WriteLine("Payment processed successfully. Your order has been placed.");
                order.UpdateStatus("Preparing");
            }
            else
            {
                Console.WriteLine("Payment processing failed. Please try again.");
            }
        }
    }

    static IPaymentProcessor GetPaymentProcessor(string method)
    {
        switch (method)
        {
            case "paypal":
                return new PayPalProcessor();
            case "visa":
                return new VisaProcessor();
            default:
                Console.WriteLine($"Payment method {method} is not supported.");
                return null;
        }
    }
}
