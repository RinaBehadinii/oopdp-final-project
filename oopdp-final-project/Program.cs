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
            var restaurants = context.Restaurants.Include(r => r.MenuItems).ToList();
            if (restaurants.Count == 0)
            {
                Console.WriteLine("No restaurants available.");
                return;
            }

            Console.WriteLine("Select a restaurant by number:");
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurants[i].Name}");
            }

            int restaurantChoice = GetUserChoice(restaurants.Count);
            if (restaurantChoice == -1) return; // Invalid choice or error

            Restaurant selectedRestaurant = restaurants[restaurantChoice];

            Console.WriteLine($"Menu for {selectedRestaurant.Name}:");
            if (selectedRestaurant.MenuItems.Count == 0)
            {
                Console.WriteLine("This restaurant has no menu items.");
                return;
            }

            foreach (var item in selectedRestaurant.MenuItems)
            {
                Console.WriteLine($"{item.ItemId}. {item.Name} - ${item.Price}");
            }

            List<MenuItem> orderedItems = GetOrderedItems(selectedRestaurant);
            if (orderedItems == null || orderedItems.Count == 0) return; // No items ordered or error

            decimal orderAmount = orderedItems.Sum(item => item.Price);
            Console.WriteLine("\nOrder Summary:");
            orderedItems.ForEach(item => Console.WriteLine($"{item.Name} - ${item.Price}"));
            Console.WriteLine($"Total Amount: ${orderAmount}");

            if (!ConfirmOrder()) return; // Order not confirmed

            string paymentMethod = ChoosePaymentMethod();
            IPaymentProcessor paymentProcessor = GetPaymentProcessor(paymentMethod);
            if (paymentProcessor == null) return; // Invalid payment method selected or error

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

            Console.WriteLine(paymentSuccess ? "Payment processed successfully. Your order has been placed." : "Payment processing failed. Please try again.");
        }
    }

    static int GetUserChoice(int count)
    {
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= count)
        {
            return choice - 1;
        }
        Console.WriteLine("Invalid selection. Please try again.");
        return -1;
    }

    static List<MenuItem> GetOrderedItems(Restaurant restaurant)
    {
        Console.WriteLine("Enter the item numbers you want to order (comma separated):");
        try
        {
            var itemNumbers = Console.ReadLine().Split(',').Select(int.Parse);
            return restaurant.MenuItems.Where(item => itemNumbers.Contains(item.ItemId)).ToList();
        }
        catch
        {
            Console.WriteLine("Invalid input. Please enter valid item numbers.");
            return null;
        }
    }

    static bool ConfirmOrder()
    {
        Console.WriteLine("Do you want to proceed with the order? (yes/no)");
        return Console.ReadLine().Trim().ToLower() == "yes";
    }

    static string ChoosePaymentMethod()
    {
        Console.WriteLine("Choose a payment method (paypal/visa):");
        return Console.ReadLine().Trim().ToLower();
    }

    static IPaymentProcessor GetPaymentProcessor(string method)
    {
        switch (method)
        {
            case "paypal": return new PayPalProcessor();
            case "visa": return new VisaProcessor();
            default:
                Console.WriteLine($"Payment method {method} is not supported.");
                return null;
        }
    }
}
