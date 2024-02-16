using oopdp_final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Observers
{
    public class CostumerNotification : IObserver
    {
        public void Update(Order order)
        {
            Console.WriteLine($"Notifying customer about order {order.OrderId} status change to {order.Status}.");
        }
    }
}
