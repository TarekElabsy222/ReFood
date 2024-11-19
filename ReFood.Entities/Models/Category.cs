using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReFood.Entities.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[DisplayName("Image")]
		[ValidateNever]
		public string ImageUrl { get; set; } 
	}
}
