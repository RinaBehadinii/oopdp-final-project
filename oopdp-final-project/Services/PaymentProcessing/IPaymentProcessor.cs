using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Services.PaymentProcessing
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, string currency);
    }
}
