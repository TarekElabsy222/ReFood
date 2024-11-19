using ReFood.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refood.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReFoodDbContext _context;        
        public ICategoryRepository Category { get; private set; }
        public IFoodItemRepository FoodItem { get; private set; }
        public UnitOfWork(ReFoodDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            FoodItem = new FoodItemRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
