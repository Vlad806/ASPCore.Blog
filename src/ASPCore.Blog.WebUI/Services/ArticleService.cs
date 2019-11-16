using System;
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
        private readonly IApplicationRepository<ArticleTags> _articleTagsRepository;

        public int pageSize = 4;

        public ArticleService(
            IApplicationRepository<Articles> articlesRepository,
            IApplicationRepository<Categories> categoriesRepository, IApplicationRepository<ArticleTags> articleTagsRepository)
        {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
            _articleTagsRepository = articleTagsRepository;
        }

        public ArticlesViewModel GetArticlesViewModel(int? categoryId, int? tagId, DateTime? start, DateTime? end, int page)
        {
            var listArticles = GetArticlesModelCollection();
            var filterArticle = categoryId == null ? listArticles : listArticles.Where(i => i.Category.CategoryId == categoryId);
            filterArticle = tagId == null ? filterArticle : GetArticlesModelCollectionByTag(tagId.Value, filterArticle);
            filterArticle = start != null && end != null
                ? GetArticlesModelCollectionByDate(start.Value, end.Value, filterArticle)
                : filterArticle;

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
                    Category = new CategoriesModel { CategoryId = item.CategoryId }
                });
            }

            return result;
        }

        private IEnumerable<ArticlesModel> GetArticlesModelCollectionByTag(int tagId, IEnumerable<ArticlesModel> articlesModels)
        {
            var listArticleTags = _articleTagsRepository.Get().Where(x => x.TagId == tagId);
            List<ArticlesModel> result = new List<ArticlesModel>();
            foreach (var item in listArticleTags)
            {
                var article = articlesModels.FirstOrDefault(x => x.ArticleId == item.ArticleId);
                result.Add(article);
            }

            return result;
        }

        private IEnumerable<ArticlesModel> GetArticlesModelCollectionByDate(DateTime start, DateTime end, IEnumerable<ArticlesModel> articlesModels)
        {
            return articlesModels.Where(x => x.DateChange >= start && x.DateChange <= end);
        }
    }
}
