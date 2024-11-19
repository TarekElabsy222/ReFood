using Microsoft.EntityFrameworkCore;
using Refood.DataAccess;
using Refood.DataAccess.Implementation;
using ReFood.Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using ReFood.Utilities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Ecommerce.Data.Cart;

namespace ReFood
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ReFoodDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(4))
				.AddDefaultTokenProviders()
				.AddDefaultUI()
				.AddEntityFrameworkStores<ReFoodDbContext>();

			builder.Services.AddSingleton<IEmailSender, EmailSender>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddScoped(x => ShoppingCart.GetShoppingCart(x));
			builder.Services.AddSession();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddRazorPages();
            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

			app.UseRouting();
			app.UseSession();
			app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
				name: "Admin",
				pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}");
			

            app.Run();
		}
	}
}
