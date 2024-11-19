using Ecommerce.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refood.DataAccess.Data.ViewComponents
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _cart;
        public ShoppingCartSummary(ShoppingCart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            var item = _cart.GetShoppingCartTotalAmount();
            ViewBag.Total = _cart.GetShoppingCartTotal();
            return View(item);
        }
    }
}
