using System.Collections.Generic;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationRepository<Categories> _categoriesRepository;

        public CategoryService(IApplicationRepository<Categories> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<CategoriesModel> GetCategoriesModelCollection()
        {
            var collection = _categoriesRepository.Get();
            List<CategoriesModel> result = new List<CategoriesModel>();
            foreach (var item in collection)
            {
                result.Add(new CategoriesModel
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name
                });
            }

            return result;
        }
    }
}
