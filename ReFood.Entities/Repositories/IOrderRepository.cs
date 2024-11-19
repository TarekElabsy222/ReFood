using ReFood.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFood.Entities.Repositories
{
	public interface IOrderRepository
	{
		void StoredOrders(List<ShoppingCartItem> items, string userId);
		List<Order> GetOrderAndRoleByUserId(string userId,string role);
	}
}
