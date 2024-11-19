using Microsoft.EntityFrameworkCore;
using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refood.DataAccess.Implementation
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ReFoodDbContext _context;
        public OrderRepository(ReFoodDbContext context)
        {
            _context = context;
        }
        public List<Order> GetOrderAndRoleByUserId(string userId,string role)
		{
			var order = _context.Orders
				.Include(x => x.orderItems)
				.ThenInclude(x => x.FoodItem)
				.Include(x=>x.User)
				.ToList();
			if(role != "Admin")
			{
				order = order.Where(x=>x.UserId == userId).ToList();
			}
			return order;
		}		 

		public void StoredOrders(List<ShoppingCartItem> items, string userId)
		{
			var order = new Order()
			{
				UserId = userId

			};
			_context.Orders.Add(order);
			_context.SaveChanges();
			foreach (var item in items)
			{
				var orderitem = new OrderItem()
				{
					Amount = item.Amount,
					Price = (double)item.FoodItem.Price,
					OrderId = order.Id,
					FoodItemId = item.FoodItem.Id
				};
				_context.OrderItems.Add(orderitem);
			}
			_context.SaveChanges();
		}
	}
}
