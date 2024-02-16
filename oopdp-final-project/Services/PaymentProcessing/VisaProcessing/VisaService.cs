using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Services.PaymentProcessing.VisaProcessing
{
    public class VisaService
    {
        public bool Charge(decimal amount, string currency)
        {
            // Logic to charge with Visa
            Console.WriteLine($"Charging {amount} {currency} using Visa...");
            return true; // Simulate a successful charge
        }
    }
}
