using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Models
{
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        
        [Column("costumer_name")]
        public string CustomerName { get; set; }
        
        [Column("costumer_address")]
        public string CustomerAddress { get; set; }
       
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        
        [Column("status")]
        public string Status { get; set; }
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
