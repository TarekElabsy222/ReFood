using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Refood.DataAccess;
using Refood.DataAccess.Implementation;
using ReFood.Entities.Enum;
using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using ReFood.Entities.ViewModels;
using System.Security.Claims;

namespace ReFood.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ReFoodDbContext _context;
        private readonly IUnitOfWork _unitofwork;
        public HomeController(IUnitOfWork unitofwork, ReFoodDbContext context)
        {
            _unitofwork = unitofwork;
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _unitofwork.Category.GetAll();
            var fooditems = _unitofwork.FoodItem.GetAll();
            var viewModel = new HomeViewModel
            {
                Categories = categories,
                FoodItems = fooditems
            };
            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var fooditem = _unitofwork.FoodItem.GetFrstOrDefault(x=>x.Id ==id,Includeword:"Category");
            return View(fooditem);
        }
        public IActionResult Category(int categoryId)
        {
            var foodItems = _unitofwork.FoodItem.GetAll(x => x.CategoryId == categoryId).Select(item => new
            {
                item.Id,
                item.Name,
                item.Description,
                item.Price,
                ImageUrl = Url.Content("/" + item.ImageUrl) // Ensure correct image URL
            });
            return Json(foodItems);
        }
        public IActionResult GetAllFoodItems()
        {
            var allFoodItems =_unitofwork.FoodItem.GetAll().Select(item => new
            {
                item.Id,
                item.Name,
                item.Description,
                item.Price,
                ImageUrl = Url.Content("/" + item.ImageUrl)
            });
            return Json(allFoodItems);
        }
        [Authorize]
        public IActionResult RecommendFoodItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (currentUser == null)
            {
                return BadRequest("User not found");
            }

            var userMedicalCondition = currentUser.MedicalConditions;

            if (userMedicalCondition == MedicalConditions.None)
            {
                ViewBag.Message = "No medical conditions specified";
                return View("RecommendFoodItems");
            }

            var recommendedOrders = _context.OrderItems
                .Where(oi => oi.Order.User.MedicalConditions == userMedicalCondition)
                .Select(oi => new FoodItemVM
                {
                    FoodItem = oi.FoodItem
                })
                .ToList();
            foreach (var item in recommendedOrders)
            {
                item.CategoryList = _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
            }

            return View("RecommendFoodItems", recommendedOrders);
        }
        // Add food item to favorites
        [HttpPost]
        public IActionResult AddToFavorite(int foodItemId)
        {
            try
            {
                // Get the current user's ID from the claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);

                // Check if user exists
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "User not found" });
                }

                // Get the food item to add to favorites
                var foodItem = _context.FoodItems.Find(foodItemId);
                if (foodItem == null)
                {
                    return Json(new { success = false, message = "Food item not found" });
                }

                // Check if the food item is already in favorites
                var existingFavorite = _context.Favorites
                    .FirstOrDefault(f => f.UserId == currentUser.Id && f.FoodItemId == foodItemId);

                if (existingFavorite != null)
                {
                    return Json(new { success = false, message = "Food item already added to favorites" });
                }

                // Create a new Favorite entry
                var favorite = new Favorite
                {
                    UserId = currentUser.Id,
                    FoodItemId = foodItemId,
                    CreatedAt = DateTime.UtcNow
                };

                // Add to the database and save changes
                _context.Favorites.Add(favorite);
                _context.SaveChanges();

                return Json(new { success = true, message = "Food item added to favorites successfully" });
            }
            catch (Exception ex)
            {
                // Log the error and return a failure message
                // _logger.LogError(ex, "An error occurred while adding the item to favorites.");
                return Json(new { success = false, message = "An error occurred while adding the item to favorites." });
            }
        }

        // Display the user's favorite food items
        public IActionResult Favorites()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);

                // Check if user exists
                if (currentUser == null)
                {
                    return BadRequest("User not found");
                }

                // Fetch the user's favorite food items
                var favorites = _context.Favorites
                    .Where(f => f.UserId == currentUser.Id)
                    .Include(f => f.FoodItem)  // Ensure food item details are loaded
                    .ToList();

                return View(favorites);
            }
            catch (Exception ex)
            {
                // Log the error and return a failure message
                // _logger.LogError(ex, "An error occurred while fetching favorites.");
                return BadRequest("An error occurred while fetching favorites.");
            }
        }

        // Remove food item from favorites
        [HttpPost]
        public IActionResult RemoveFromFavorites(int foodItemId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Find the favorite entry for the current user and food item
                var favorite = _context.Favorites
                    .FirstOrDefault(f => f.UserId == userId && f.FoodItemId == foodItemId);

                // If favorite exists, remove it
                if (favorite != null)
                {
                    _context.Favorites.Remove(favorite);
                    _context.SaveChanges();
                }

                // Redirect back to the favorites page
                return RedirectToAction("Favorites");
            }
            catch (Exception ex)
            {
                // Log the error and return a failure message
                // _logger.LogError(ex, "An error occurred while removing the item from favorites.");
                return BadRequest("An error occurred while removing the item from favorites.");
            }
        }




    }
}

