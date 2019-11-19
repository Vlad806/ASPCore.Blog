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
        private readonly IApplicationRepository<Tags> _tagsRepository;

        public int pageSize = 4;

        public ArticleService(
            IApplicationRepository<Articles> articlesRepository,
            IApplicationRepository<Categories> categoriesRepository,
            IApplicationRepository<Tags> tagsRepository,
            IApplicationRepository<ArticleTags> articleTagsRepository)
        {
            _articlesRepository = articlesRepository;
            _categoriesRepository = categoriesRepository;
            _articleTagsRepository = articleTagsRepository;
            _tagsRepository = tagsRepository;
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
            var tags = _tagsRepository.Get();
            var tagsCollection = GetTagsCollectionByArticles(id, tags);
            var model = new ArticlesModel
            {
                ArticleId = article.ArticleId,
                DateChange = article.DateChange,
                Description = article.Description,
                HeroImage = article.HeroImage,
                Name = article.Name,
                ShortDescription = article.ShortDescription,
                Tags = tagsCollection,
                Category = new CategoriesModel { CategoryId = category.CategoryId, Name = category.Name },
                SelectedTags = tagsCollection.Select(x => x.TagId)
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

        public IEnumerable<ArticlesModel> GetArticlesTable()
        {
            var articles = _articlesRepository.Get();
            var tags = _tagsRepository.Get();
            var categories = _categoriesRepository.Get();
            return articles.Select(p => new ArticlesModel
            {
                Name = p.Name,
                ArticleId = p.ArticleId,
                Description = p.Description,
                ShortDescription = p.ShortDescription,
                DateChange = p.DateChange,
                HeroImage = p.HeroImage,
                Category = categories.Where(c => c.CategoryId == p.CategoryId).Select(nc => new CategoriesModel()
                {
                    CategoryId = nc.CategoryId,
                    Name = nc.Name
                }).FirstOrDefault(),
                Tags = GetTagsCollectionByArticles(p.ArticleId, tags)
            }).ToList();

        }

        public void SaveArticle(ArticlesModel model)
        {
            var date = DateTime.Now;
            var article = new Articles
            {
                DateChange = date,
                Description = model.Description,
                Name = model.Name,
                ShortDescription = model.ShortDescription,
                CategoryId = model.Category.CategoryId
            };

            _articlesRepository.Insert(article);
            var addedArticle = _articlesRepository.Get().FirstOrDefault(x => x.DateChange == date && x.Name == model.Name);
            SaveArticleTags(model.SelectedTags, addedArticle);
        }

        public void UpdateArticle(ArticlesModel model)
        {
            var article = _articlesRepository.GetByID(model.ArticleId);
            article.Name = model.Name;
            article.ShortDescription = model.ShortDescription;
            article.Description = model.Description;
            article.CategoryId = model.Category.CategoryId;
            article.DateChange = DateTime.Now;

            _articlesRepository.Update(article);
            var tagCollect = _articleTagsRepository.Get().Where(t => t.ArticleId == article.ArticleId);
            foreach (var articleTag in tagCollect)
            {
                _articleTagsRepository.Delete(articleTag.ArticleId, articleTag.TagId);
            }
            SaveArticleTags(model.SelectedTags, article);
        }

        public void DeleteArticle(int id)
        {
            var artTagCollect = _articleTagsRepository.Get().Where(t => t.ArticleId == id);
            foreach (var articleTag in artTagCollect)
            {
                _articleTagsRepository.Delete(articleTag.ArticleId, articleTag.TagId);
            }
            _articlesRepository.Delete(id);
        }

        private void SaveArticleTags(IEnumerable<int> selectedTags, Articles article)
        {
            foreach (var tagId in selectedTags)
            {
                _articleTagsRepository.Insert(new ArticleTags { ArticleId = article.ArticleId, TagId = tagId });
            }
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

        private IEnumerable<Tags> GetTagsCollectionByArticles(int articleId, IEnumerable<Tags> tags)
        {
            var listArticleTags = _articleTagsRepository.Get().Where(x => x.ArticleId == articleId);
            List<Tags> result = new List<Tags>();
            foreach (var item in listArticleTags)
            {
                var listTags = tags.FirstOrDefault(x => x.TagId == item.TagId);
                result.Add(listTags);
            }

            return result;
        }

        private IEnumerable<ArticlesModel> GetArticlesModelCollectionByDate(DateTime start, DateTime end, IEnumerable<ArticlesModel> articlesModels)
        {
            return articlesModels.Where(x => x.DateChange.Date >= start.Date && x.DateChange.Date <= end.Date);
        }
    }
}
