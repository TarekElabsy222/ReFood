using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refood.DataAccess.Implementation
{
    public class FoodItemRepository : GenericRepository<FoodItem>, IFoodItemRepository
    {
        private readonly ReFoodDbContext _context;
        public FoodItemRepository(ReFoodDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(FoodItem fooditem)
        {
            var fooditemInDb = _context.FoodItems.FirstOrDefault(x=>x.Id == fooditem.Id);
            if (fooditemInDb != null)
            {
                fooditemInDb.Name = fooditem.Name;
                fooditemInDb.Description = fooditem.Description;
                fooditemInDb.ImageUrl = fooditem.ImageUrl;
                fooditemInDb.Price = fooditem.Price;
                fooditemInDb.Calories = fooditem.Calories;
                fooditemInDb.Fat = fooditem.Fat;
                fooditemInDb.Carbs = fooditem.Carbs;
                fooditemInDb.Protein = fooditem.Protein;
				fooditemInDb.CategoryId = fooditem.CategoryId;

			}
        }
    }
}
