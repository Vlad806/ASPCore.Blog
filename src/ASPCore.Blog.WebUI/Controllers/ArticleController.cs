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
        public IActionResult ArticlesList(int? id, int page = 1)
        {
            return View(_articleService.GetArticlesViewModel(id, page));
        }

        [HttpGet]
        public IActionResult ArticleDetails(int id)
        {
            return View(_articleService.GetArticle(id));
        }
    }
}