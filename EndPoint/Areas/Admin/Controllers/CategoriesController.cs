using Application.Interfaces.Context;
using Application.Services.Categories.Commands;
using Application.Services.Categories.Commands.AddNewCategory;
using Application.Services.Categories.Commands.EditCategory;
using Application.Services.Categories.Commands.RemoveCategory;
using Application.Services.Categories.Queries.GetCategories;
using Microsoft.AspNetCore.Mvc;

namespace testWebsit.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IGetCategories _context;
        private readonly IAddNewCategory _addNewCategory;
        private readonly IRemoveCategory _removeCategory;
        private readonly IEditCategory _editCategory;
        public CategoriesController(IGetCategories context,
            IAddNewCategory addNewCategory,
            IRemoveCategory removeCategory,
            IEditCategory editCategory)
        {
            _context = context;
            _addNewCategory = addNewCategory;
            _removeCategory = removeCategory;
            _editCategory = editCategory;
        }
        public IActionResult Index(long? parentId)
        {
            return View(_context.ExecutResult(parentId).Date);
        }
        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewCategory(long? parentId,string name)
        {
            var result = _addNewCategory.ExecutResult(parentId, name);
            return Json(result);
        }
        [HttpPost]
        public IActionResult RemoveCategory(long Id)
        {
            var result = _removeCategory.ExecutResult(Id);
            return Json(result);
        }

        [HttpGet]
        public IActionResult EditCategory(long? parentId, string name)
        {
            ViewBag.parentId = parentId;
            ViewBag.name = name;
            return View();
        }

        [HttpPost]
        public IActionResult EditCategory(long parentId, string name)
        {
            var result = _editCategory.resultDto(parentId, name);
            return Json(result);
        }
    }
}
