using oopdp_final_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopdp_final_project.Entities
{
    public class Restaurant
    {
        [Key]
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
       
        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}
