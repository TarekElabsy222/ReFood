using ReFood.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Repositories
{
    public interface IFoodItemRepository:IGenericRepository<FoodItem>
    {
        void Update(FoodItem fooditem);
    }
}
