using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public interface IArticleService
    {
        ArticlesModel GetArticlesModel(int? id, int page);
    }
}
