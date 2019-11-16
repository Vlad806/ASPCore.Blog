using System.Collections.Generic;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public interface IArticleService
    {
        ArticlesViewModel GetArticlesViewModel(int? id, int page);
        ArticlesModel GetArticle(int id);
        IEnumerable<ArticlesModel> GetArticlesModelCollection();
    }
}
