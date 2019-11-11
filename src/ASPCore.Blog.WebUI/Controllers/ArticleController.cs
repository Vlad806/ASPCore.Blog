using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IApplicationRepository<Articles> _articlesRepository;
        public int pageSize = 4;

        public ArticleController(IApplicationRepository<Articles> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ArticlesList(int page = 1)
        {
            var list = _articlesRepository.Get();
            var articlesModel = new ArticlesModel
            {
                Articles = list
                    .OrderBy(article => article.ArticleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = list.Count()
                }
            };
            return View(articlesModel);
        }
    }
}