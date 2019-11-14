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
        private readonly IApplicationRepository<Categories> _categoriesRepository;
        private readonly IApplicationRepository<Tags> _tagsRepository;
        public int pageSize = 4;

        public ArticleController(
            IApplicationRepository<Articles> articlesRepository,
                IApplicationRepository<Categories> categoriesRepository,
                    IApplicationRepository<Tags> tagsRepository
            )
        {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
            _tagsRepository = tagsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ArticlesList(int id, int page = 1)
        {
            var listArticles = _articlesRepository.Get();
            var listCategories = _categoriesRepository.Get();
            var listTags = _tagsRepository.Get();
            var articlesModel = new ArticlesModel
            {
               
                Articles = listArticles
                    .Where(i => i.CategoryId == id)
                    .OrderBy(article => article.ArticleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = listArticles.Count()
                },
                Categories = listCategories,
                Tags = listTags
            };

            return View(articlesModel);
        }
    }
}