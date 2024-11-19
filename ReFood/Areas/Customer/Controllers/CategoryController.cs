using Microsoft.AspNetCore.Mvc;
using Refood.DataAccess;
using ReFood.Entities.Models;

namespace ReFood.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {
        private readonly ReFoodDbContext _context;
        public CategoryController(ReFoodDbContext context)
        {
            _context = context;
        }
        public IActionResult ShowCategory()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Getfood(int categoryId)
        {
            List<FoodItem> food = _context.FoodItems.Where(f => f.CategoryId == categoryId).ToList();
            return Json(food);
        }
    }
}
