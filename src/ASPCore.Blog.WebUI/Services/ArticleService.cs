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
        public int pageSize = 4;

        public ArticleService(
            IApplicationRepository<Articles> articlesRepository,
            IApplicationRepository<Categories> categoriesRepository
        )
        {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
        }

        public ArticlesViewModel GetArticlesViewModel(int? id, int page)
        {
            var listArticles = GetArticlesModelCollection();
            var filterArticle = id == null ? listArticles : listArticles.Where(i => i.Category.CategoryId == id);

            var articlesModel = new ArticlesViewModel
            {
                Articles = filterArticle
                    .OrderBy(article => article.ArticleId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = filterArticle.Count()
                }
            };
            return articlesModel;
        }

        public ArticlesModel GetArticle(int id)
        {
            var article = _articlesRepository.GetByID(id);
            var category = _categoriesRepository.GetByID(article.CategoryId);
            var model = new ArticlesModel
            {
                ArticleId = article.ArticleId,
                DateChange = article.DateChange,
                Description = article.Description,
                HeroImage = article.HeroImage,
                Name = article.Name,
                ShortDescription = article.ShortDescription,
                Category = new CategoriesModel { CategoryId = category.CategoryId, Name = category.Name }
            };

            return model;
        }

        public IEnumerable<ArticlesModel> GetArticlesModelCollection()
        {
            var collection = _articlesRepository.Get();
            List<ArticlesModel> result = new List<ArticlesModel>();
            foreach (var item in collection)
            {
                result.Add(new ArticlesModel
                {
                    ArticleId = item.ArticleId,
                    DateChange = item.DateChange,
                    Description = item.Description,
                    HeroImage = item.HeroImage,
                    Name = item.Name,
                    ShortDescription = item.ShortDescription,
                    Category = new CategoriesModel { CategoryId = item.CategoryId}
                });
            }

            return result;
        }
    }
}
