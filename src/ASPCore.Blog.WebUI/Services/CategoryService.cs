using System.Collections.Generic;
using System.Linq;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCore.Blog.WebUI.Services
{
    public class CategoryService : ICategoryService
    {
        private const int defaultCategory = 7;

        private readonly IApplicationRepository<Categories> _categoriesRepository;
        private readonly IApplicationRepository<Articles> _articlesRepository;

        public CategoryService(IApplicationRepository<Categories> categoriesRepository, IApplicationRepository<Articles> articlesRepository)
        {
            _categoriesRepository = categoriesRepository;
            _articlesRepository = articlesRepository;
        }

        public IEnumerable<CategoriesModel> GetCategoriesModelCollection()
        {
            var categories = _categoriesRepository.Get();
            return categories.Select(c => new CategoriesModel()
            {
                CategoryId = c.CategoryId,
                Name = c.Name
            });
        }

        public List<SelectListItem> GetCategoriesSelectedList(int? selectedItemId = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var categories = _categoriesRepository.Get().ToList();

            foreach (var category in categories)
            {
                var selected = category.CategoryId == selectedItemId;
                list.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.Name,
                    Selected = selected
                });
            }

            return list;
        }

        public void SaveCategory(CategoriesModel model)
        {
            var category = new Categories()
            {
                Name = model.Name
            };

            _categoriesRepository.Insert(category);
        }

        public void UpdateCategory(CategoriesModel model)
        {
            var category = _categoriesRepository.GetByID(model.CategoryId);
            category.Name = model.Name;

            _categoriesRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            if (id != defaultCategory)
            {
                var articlesCollect = _articlesRepository.Get().Where(t => t.CategoryId == id).ToList();
                foreach (var article in articlesCollect)
                {
                    article.CategoryId = defaultCategory;
                    _articlesRepository.Update(article);
                }
                _categoriesRepository.Delete(id);
            }
        }

        public CategoriesModel GetCategory(int id)
        {
            var category = _categoriesRepository.GetByID(id);
            var model = new CategoriesModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return model;
        }
    }
}
