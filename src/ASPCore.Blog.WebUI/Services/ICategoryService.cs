using System.Collections.Generic;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoriesModel> GetCategoriesModelCollection();
    }
}
