using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using oopdp_final_project.DatabaseConnection;
using oopdp_final_project.Entities;
using oopdp_final_project.Models;
using oopdp_final_project.Observers;
using oopdp_final_project.Services.PaymentProcessing;
using oopdp_final_project.Services.PaymentProcessing.PayPalProcessing;
using oopdp_final_project.Services.PaymentProcessing.VisaProcessing;

//IPaymentProcessor paymentProcessor = GetPaymentProcessor("paypal");

using (var context = DatabaseManager.Instance)
{
    // Display restaurants and let the user choose one
    Console.WriteLine("Select a restaurant by number:");
    var restaurants = context.Restaurants.Include(r => r.MenuItems).ToList();
    for (int i = 0; i < restaurants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {restaurants[i].Name}");
    }

    int restaurantChoice = Convert.ToInt32(Console.ReadLine()) - 1;
    Restaurant selectedRestaurant = restaurants[restaurantChoice];

    // Display menu items from the selected restaurant
    Console.WriteLine($"Menu for {selectedRestaurant.Name}:");
    foreach (var item in selectedRestaurant.MenuItems)
    {
        Console.WriteLine($"{item.ItemId}. {item.Name} - ${item.Price}");
    }

    // Let the user choose items to order
    Console.WriteLine("Enter the item numbers you want to order (comma separated):");
    var itemNumbers = Console.ReadLine().Split(',').Select(int.Parse).ToList();
    List<MenuItem> orderedItems = selectedRestaurant.MenuItems
        .Where(item => itemNumbers.Contains(item.ItemId))
        .ToList();

    // Calculate total order amount
    decimal orderAmount = orderedItems.Sum(item => item.Price);

    // Process payment (simplified)
    string paymentMethod = "paypal";
    IPaymentProcessor paymentProcessor = GetPaymentProcessor(paymentMethod);
    bool paymentSuccess = paymentProcessor.ProcessPayment(orderAmount, "USD");

    // Create and process the order (simplified)
    Order order = new Order
    {
        CustomerName = "John Doe",
        CustomerAddress = "123 Main St",
        TotalPrice = orderAmount,
        Status = paymentSuccess ? "Paid" : "Payment Failed",
        CreatedAt = DateTime.Now
        // Attach observers and process the order accordingly
    };
    order.Attach(new RestaurantNotification()); // Simulate attaching an observer

    if (paymentSuccess)
    {
        Console.WriteLine("Payment processed successfully. Your order has been placed.");
        order.UpdateStatus("Preparing"); // Notify observers
    }
    else
    {
        Console.WriteLine("Payment processing failed. Please try again.");
    }
}

static IPaymentProcessor GetPaymentProcessor(string method)
{
    switch (method.ToLower())
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

static bool ProcessPayment(IPaymentProcessor processor, decimal amount, string currency)
{
    return processor.ProcessPayment(amount, currency);
}