using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReFood.Entities.Models;

namespace Refood.DataAccess
{
	public class ReFoodDbContext:IdentityDbContext
	{
        public ReFoodDbContext(DbContextOptions<ReFoodDbContext> options):base(options)
        {            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
   