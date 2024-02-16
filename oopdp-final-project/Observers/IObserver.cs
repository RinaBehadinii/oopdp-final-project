using oopdp_final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Observers
{
    public interface IObserver
    {
        void Update(Order order);
    }
}
