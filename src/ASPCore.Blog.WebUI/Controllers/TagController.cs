using ASPCore.Blog.WebUI.Models;
using ASPCore.Blog.WebUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPCore.Blog.WebUI.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagsService _tagsService;

        public TagController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_tagsService.GetTagsModelCollection());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("TagForm");
        }

        [HttpPost]
        public IActionResult Add(TagsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            _tagsService.SaveTag(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Action = "Update";
            var model = _tagsService.GetTag(id);
            return View("TagForm", model);
        }

        [HttpPost]
        public IActionResult Update(TagsModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update");
            }

            _tagsService.UpdateTag(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _tagsService.DeleteTag(id);
            return RedirectToAction("Index");
        }
    }
}