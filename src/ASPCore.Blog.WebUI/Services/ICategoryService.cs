using System.Collections.Generic;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCore.Blog.WebUI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoriesModel> GetCategoriesModelCollection();
        List<SelectListItem> GetCategoriesSelectedList(int? selectedItemId = null);
        void SaveCategory(CategoriesModel model);
        void UpdateCategory(CategoriesModel model);
        void DeleteCategory(int id);
        CategoriesModel GetCategory(int id);
    }
}
