using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Refood.DataAccess;
using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using ReFood.Entities.ViewModels;


namespace ReFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public CategoryController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
			_webHostEnvironment = webHostEnvironment;
		}
        public IActionResult Index()
        {
            var category = _unitofwork.Category.GetAll();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category, IFormFile file)
        {
            if (ModelState.IsValid)
            {
				string RootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString();
					var Upload = Path.Combine(RootPath, @"Images\Categories");
					var ext = Path.GetExtension(file.FileName);

					using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					category.ImageUrl = @"Images\Categories\" + filename + ext;
				}

				//_context.Categories.Add(category);
				_unitofwork.Category.Add(category);
                //_context.SaveChanges();
                _unitofwork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                NotFound();
            }
            //var category = _context.Categories.Find(id);
            var category = _unitofwork.Category.GetFrstOrDefault(x => x.Id == id);
            return View(nameof(Edit), category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(category);
                _unitofwork.Category.Update(category);
                _unitofwork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                NotFound();
            }
            var category = _unitofwork.Category.GetFrstOrDefault(x => x.Id == id);
            _unitofwork.Category.Remove(category);
            _unitofwork.Complete();
            TempData["Message"] = "Data Has Deleted Succesfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
