using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        // Navegation Bar
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        public int FoodItemId { get; set; }
        [ForeignKey(nameof(FoodItemId))]
        public virtual FoodItem FoodItem { get; set; }
    }
}
