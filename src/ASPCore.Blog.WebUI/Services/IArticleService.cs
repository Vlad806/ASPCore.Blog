using System;
using System.Collections.Generic;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public interface IArticleService
    {
        ArticlesViewModel GetArticlesViewModel(int? categoryId, int? tagId, DateTime? start, DateTime? end, int page);
        ArticlesModel GetArticle(int id);
        IEnumerable<ArticlesModel> GetArticlesModelCollection();
        IEnumerable<ArticlesModel> GetArticlesTable();
        void SaveArticle(ArticlesModel model);
        void UpdateArticle(ArticlesModel model);
        void DeleteArticle(int id);
    }
}
