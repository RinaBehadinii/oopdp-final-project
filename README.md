
# Food Delivery Service

## Project Overview
This project is a simple console-based application that simulates a food delivery service. Users can choose a restaurant, select menu items to order, and choose a payment method to complete their order. The application demonstrates the use of several design patterns, including Singleton, Observer, and Adapter (through payment processing). It's developed using C# and Entity Framework Core for database interactions.

## Design Patterns Used

### Singleton
The Singleton pattern is used to ensure that only one instance of the `DatabaseManager` class exists throughout the application's lifecycle. This instance manages the database context (`DatabaseContext`) for accessing and manipulating the database.

**Implementation**: The `DatabaseManager` class implements Singleton by providing a static method `Instance` that returns the same `DatabaseContext` instance every time it is called.

### Observer
The Observer pattern is used to notify various parts of the system about changes in order status. When an order's status is updated (e.g., from "Pending" to "Preparing"), all registered observers are notified of this change, allowing for actions such as updating the user interface or sending notifications.

**Implementation**: The `Order` class acts as the subject, and various observer classes (e.g., `RestaurantNotification`) implement the `IObserver` interface. Observers are attached to an order and are notified whenever the order's status changes.

### Adapter (Payment Processing)
The Strategy pattern is used to select the payment processing strategy at runtime based on the user's choice. This allows for flexibility in adding new payment methods without modifying the core payment processing logic.

**Implementation**: The `IPaymentProcessor` interface defines the contract for payment processors. Concrete classes (e.g., `PayPalProcessor`, `VisaProcessor`) implement this interface, encapsulating the specific payment processing logic. The choice of payment processor is made at runtime based on user input.

## How to Run
1. Ensure .NET 5.0 or later is installed on your machine.
2. Clone the repository to your local machine.
3. Navigate to the project directory in your terminal or command prompt.
4. Run `dotnet restore` to install necessary packages.
5. Run `dotnet run` to start the application.
6. Follow the on-screen prompts to place an order.

## Dependencies
- .NET 5.0 or later
- Entity Framework Core
- Microsoft SQL Server (LocalDB or other editions)

## Future Enhancements
- Implementing a web-based interface using ASP.NET Core.
- Adding authentication and user management.
- Expanding the payment processing strategies to include more options such as Stripe.
