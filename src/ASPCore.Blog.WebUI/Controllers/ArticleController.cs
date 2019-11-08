using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPCore.Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IApplicationRepository<Articles> _articlesRepository;

        public ArticleController(IApplicationRepository<Articles> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ArticlesList()
        {
            var list = _articlesRepository.Get();
            return View(list);
        }
    }
}