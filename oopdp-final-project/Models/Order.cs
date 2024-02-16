using oopdp_final_project.Observers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oopdp_final_project.Models
{
    public class Order : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_address")]
        public string CustomerAddress { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        private string status;
        [Column("status")]
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    NotifyObservers();
                }
            }
        }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}
