using System.Collections.Generic;
using System.Linq;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IApplicationRepository<Articles> _articlesRepository;
        private readonly IApplicationRepository<Categories> _categoriesRepository;
        private readonly IApplicationRepository<Tags> _tagsRepository;
        public int pageSize = 4;

        public ArticleService(
            IApplicationRepository<Articles> articlesRepository,
            IApplicationRepository<Categories> categoriesRepository,
            IApplicationRepository<Tags> tagsRepository
        )
        {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
            _tagsRepository = tagsRepository;
        }

        public ArticlesModel GetArticlesModel(int? id, int page)
        {
            var listArticles = _articlesRepository.Get();
            var listCategories = _categoriesRepository.Get();
            var listTags = _tagsRepository.Get();
            IEnumerable<Articles> filterArticle;
            if (id == null)
            {
                filterArticle = listArticles
                    .OrderBy(article => article.ArticleId);
            }
            else
            {
                filterArticle = listArticles
                    .Where(i => i.CategoryId == id)
                    .OrderBy(article => article.ArticleId);
            }
            var articlesModel = new ArticlesModel
            {
                Articles = filterArticle
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = filterArticle.Count()
                },
                Categories = listCategories,
                Tags = listTags
            };
            return articlesModel;
        }
    }
}
