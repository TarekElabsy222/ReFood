using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refood.DataAccess.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ReFoodDbContext _context;
        public CategoryRepository(ReFoodDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categoryInDb = _context.Categories.FirstOrDefault(x=>x.Id == category.Id);
            if (categoryInDb != null)
            {
                categoryInDb.Name = category.Name;
                categoryInDb.ImageUrl = category.ImageUrl;
            }
        }
    }
}
