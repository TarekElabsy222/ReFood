using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReFood.Entities.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        //public IsVegan IsVegan { get; set; }
        public decimal Calories { get; set; }
        public decimal Fat { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
		//Navegation Property
		[DisplayName("Category")]
		public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
		[ValidateNever]
		public Category Category { get; set; }
    }
}
