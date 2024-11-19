using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public FoodItem FoodItem { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
