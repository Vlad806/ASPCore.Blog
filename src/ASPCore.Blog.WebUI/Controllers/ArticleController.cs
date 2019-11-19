using System;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using ASPCore.Blog.WebUI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ASPCore.Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagsService _tagsService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, ITagsService tagsService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _tagsService = tagsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ArticlesTable()
        {
            return View(_articleService.GetArticlesTable());
        }

        [HttpGet]
        public IActionResult ArticlesList(int? categoryId, int? tagId, DateTime? start, DateTime? end, int page = 1)
        {
            return View(_articleService.GetArticlesViewModel(categoryId, tagId, start, end, page));
        }

        [HttpGet]
        public IActionResult ArticleDetails(int id)
        {
            return View(_articleService.GetArticle(id));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = _categoryService.GetCategoriesSelectedList();
            ViewBag.Tags = _tagsService.GeTagsSelectedList();
            return View("ArticleForm");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ArticlesModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            _articleService.SaveArticle(model);
            return RedirectToAction("ArticlesTable");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int id)
        {
            ViewBag.Action = "Update";
            ViewBag.Categories = _categoryService.GetCategoriesSelectedList(id);
            var model = _articleService.GetArticle(id);
            ViewBag.Tags = _tagsService.GeTagsSelectedList(model.SelectedTags);
            return View("ArticleForm", model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ArticlesModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Update");
            }

            _articleService.UpdateArticle(model);
            return RedirectToAction("ArticlesTable");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _articleService.DeleteArticle(id);
            return RedirectToAction("ArticlesTable");
        }
    }
}