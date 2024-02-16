using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Services.PaymentProcessing.PayPalProcessing
{
    public class PayPalService
    {
        public bool MakePayment(decimal amount, string currency)
        {
            // Logic to process payment with PayPal
            Console.WriteLine($"Processing {amount} {currency} payment with PayPal...");
            return true; // Simulate a successful payment
        }
    }
}
