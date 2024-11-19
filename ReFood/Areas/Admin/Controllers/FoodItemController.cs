using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Refood.DataAccess;
using ReFood.Entities.Models;
using ReFood.Entities.Repositories;
using ReFood.Entities.ViewModels;


namespace ReFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FoodItemController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        public FoodItemController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
			_webHostEnvironment = webHostEnvironment;
		}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetData()
        {
            var fooditems = _unitofwork.FoodItem.GetAll(Includeword:"Category");
            var recordsTotal = fooditems.Count();
            var jsonData = new {recordsFiltered = recordsTotal,recordsTotal,data = fooditems};
            return Ok(jsonData);
        }
        public IActionResult Create()
        {
            FoodItemVM fooditemVM = new FoodItemVM()
            {
                FoodItem = new FoodItem(),
                CategoryList = _unitofwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(fooditemVM);
        }
        [HttpPost]
        public IActionResult Create(FoodItemVM fooditemVM,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(RootPath,@"Images\FoodItems" );
                    var ext = Path.GetExtension(file.FileName);

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + ext),FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    fooditemVM.FoodItem.ImageUrl = @"Images\FoodItems\" + filename + ext;
                }

                _unitofwork.FoodItem.Add(fooditemVM.FoodItem);
                _unitofwork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(fooditemVM.FoodItem);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            FoodItemVM fooditemVM = new FoodItemVM()
            {
                FoodItem = _unitofwork.FoodItem.GetFrstOrDefault(x => x.Id == id),
                CategoryList = _unitofwork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(fooditemVM);
        }
        [HttpPost]
        public IActionResult Edit(FoodItemVM fooditemVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
				string RootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString();
					var Upload = Path.Combine(RootPath, @"Images\FoodItems");
					var ext = Path.GetExtension(file.FileName);

                    if (fooditemVM.FoodItem.ImageUrl != null)
                    {
                        var oldimg = Path.Combine(RootPath,fooditemVM.FoodItem.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimg))
                        {
							System.IO.File.Delete(oldimg);

						}
                    }

					using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					fooditemVM.FoodItem.ImageUrl = @"Images\FoodItems\" + filename + ext;
				}

				//_context.Categories.Update(fooditem);
				_unitofwork.FoodItem.Update(fooditemVM.FoodItem);
                _unitofwork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(fooditemVM.FoodItem);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            var fooditem = _unitofwork.FoodItem.GetFrstOrDefault(x => x.Id == id);
            _unitofwork.FoodItem.Remove(fooditem);
			var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, fooditem.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldimg))
			{
				System.IO.File.Delete(oldimg);

			}
			_unitofwork.Complete();
			return Json(new {success = true, message = "Food item has been Deleted"});            
        }
    }
}
