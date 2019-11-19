using ASPCore.Blog.WebUI.Models;
using ASPCore.Blog.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPCore.Blog.WebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_categoryService.GetCategoriesModelCollection());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("CategoryForm");
        }

        [HttpPost]
        public IActionResult Add(CategoriesModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            _categoryService.SaveCategory(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Action = "Update";
            var model = _categoryService.GetCategory(id);
            return View("CategoryForm", model);
        }

        [HttpPost]
        public IActionResult Update(CategoriesModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update");
            }

            _categoryService.UpdateCategory(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}