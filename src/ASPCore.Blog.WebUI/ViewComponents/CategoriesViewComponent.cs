using System.Threading.Tasks;
using ASPCore.Blog.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ASPCore.Blog.WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Task<ViewViewComponentResult> InvokeAsync()
        {
            return Task.FromResult(View(_categoryService.GetCategoriesModelCollection()));
        }
    }
}
