using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ReFood.Entities.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } 
        [Required]
        [ValidateNever]
        public int FoodItemId { get; set; } 

        public DateTime CreatedAt { get; set; } 

        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("FoodItemId")]
        [ValidateNever]
        public virtual FoodItem FoodItem { get; set; }

    }
}
