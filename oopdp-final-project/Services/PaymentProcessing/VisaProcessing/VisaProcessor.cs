using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Services.PaymentProcessing.VisaProcessing
{
    public class VisaProcessor : IPaymentProcessor
    {
        private VisaService _visaService = new VisaService();

        public bool ProcessPayment(decimal amount, string currency)
        {
            return _visaService.Charge(amount, currency);
        }
    }
}
