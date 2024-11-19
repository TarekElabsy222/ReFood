using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Models
{
    public class Order
    {
        public Order()
        {
            orderItems = new HashSet<OrderItem>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
    }
}
