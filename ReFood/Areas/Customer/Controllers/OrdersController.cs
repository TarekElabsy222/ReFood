using Ecommerce.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using ReFood.Entities.Repositories;
using System.Security.Claims;

namespace ReFood.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrdersController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IUnitOfWork _unitOfWork;
		private readonly IOrderRepository _orderServices;
		public OrdersController( ShoppingCart shoppingCart,IUnitOfWork unitOfWork, IOrderRepository orderServices)
        {
            _shoppingCart = shoppingCart;
			_unitOfWork = unitOfWork;
            _orderServices = orderServices;
		}
		public IActionResult Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string roleId = User.FindFirstValue(ClaimTypes.Role);
			var order = _orderServices.GetOrderAndRoleByUserId(userId,roleId);
			return View(order);
		}
		public IActionResult ShoppingCart()
        {
            var item = _shoppingCart.GetShoppingCartItems();
            ViewBag.Total = _shoppingCart.GetShoppingCartTotal();
            return View(item);
        }
        public IActionResult AddToCart(int id)
        {
            var item =  _unitOfWork.FoodItem.GetFrstOrDefault(x => x.Id == id);
			if (item != null)
			{
				 _shoppingCart.AddItemToShoppingCart(item);
			}
			return RedirectToAction("ShoppingCart");
		}
        public IActionResult RemoveFromCart(int id)
        {
            var item = _unitOfWork.FoodItem.GetFrstOrDefault(x => x.Id == id);
            if (item != null)
            {
               _shoppingCart.RemoveItemFromShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
		public IActionResult CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _orderServices.StoredOrders(items, userId);
            }
            else
            {
                string userId ="";
                _orderServices.StoredOrders(items, userId);
            }
            
			_shoppingCart.ClearShoppingCart();
			return View("CompleteOrder");
		}
	}
}
