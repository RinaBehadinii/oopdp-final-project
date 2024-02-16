using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Services.PaymentProcessing.PayPalProcessing
{
    public class PayPalProcessor : IPaymentProcessor
    {
        private PayPalService _payPalService = new PayPalService();

        public bool ProcessPayment(decimal amount, string currency)
        {
            return _payPalService.MakePayment(amount, currency);
        }
    }
}
