using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refood.DataAccess;
using ReFood.Entities.Repositories;
using System.Security.Claims;

namespace ReFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ReFoodDbContext _context;
        private readonly IUnitOfWork _unitofwork;
        private readonly IOrderRepository _orderServices;
        public OrdersController(IUnitOfWork unitofwork, ReFoodDbContext context, IOrderRepository orderServices)
        {
            _unitofwork = unitofwork;
            _context = context;
            _orderServices = orderServices;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string roleId = User.FindFirstValue(ClaimTypes.Role);
            var order = _orderServices.GetOrderAndRoleByUserId(userId, roleId);
            return View(order);
        }
    }
}
