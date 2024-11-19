using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refood.DataAccess;
using ReFood.Entities.Repositories;
using ReFood.Utilities;

namespace ReFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ReFoodDbContext _context;
        private readonly IUnitOfWork _unitofwork;
        public DashboardController(IUnitOfWork unitofwork, ReFoodDbContext context)
        {
            _unitofwork = unitofwork;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.FoodItems = _unitofwork.FoodItem.GetAll().Count();
            ViewBag.Users = _context.ApplicationUsers.ToList().Count();
            ViewBag.Orders = _context.Orders.ToList().Count();
            ViewBag.Category = _unitofwork.Category.GetAll().Count();
            ViewBag.Order = _context.Orders.ToList().Count();
            return View();
        }
    }
}
