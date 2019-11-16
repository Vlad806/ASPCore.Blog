using System;
using Microsoft.AspNetCore.Mvc;
using ASPCore.Blog.WebUI.Services;

namespace ASPCore.Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}