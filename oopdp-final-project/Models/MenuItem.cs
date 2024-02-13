using oopdp_final_project.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Models
{
    public class MenuItem
    {
        [Key]
        [Column("item_id")]
        public int ItemId { get; set; }
        
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        [Column("desciption")]
        public string Description { get; set; }
       
        [Column("price")]
        public decimal Price { get; set; }

        public Restaurant Restaurant { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
