using System.Collections.Generic;
using System.Linq;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCore.Blog.WebUI.Services
{
    public class TagsService : ITagsService
    {
        private readonly IApplicationRepository<Tags> _tagsRepository;
        private readonly IApplicationRepository<Articles> _articlesRepository;
        private readonly IApplicationRepository<ArticleTags> _articleTagsRepository;

        public TagsService(IApplicationRepository<Tags> tagsRepository, IApplicationRepository<Articles> articlesRepository, IApplicationRepository<ArticleTags> articleTagsRepository)
        {
            _tagsRepository = tagsRepository;
            _articlesRepository = articlesRepository;
            _articleTagsRepository = articleTagsRepository;
        }

        public IEnumerable<TagsModel> GetTagsModelCollection()
        {
            var collection = _tagsRepository.Get();
            List<TagsModel> result = new List<TagsModel>();
            foreach (var item in collection)
            {
                result.Add(new TagsModel
                {
                    TagId = item.TagId,
                    Name = item.Name
                });
            }

            return result;
        }

        public List<SelectListItem> GeTagsSelectedList(IEnumerable<int> selectedItemId = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var tags = _tagsRepository.Get().ToList();

            foreach (var tag in tags)
            {
                var selected = selectedItemId?.Contains(tag.TagId) ?? false;
                list.Add(new SelectListItem
                {
                    Value = tag.TagId.ToString(),
                    Text = tag.Name,
                    Selected = selected
                });
            }

            return list;
        }

        public void SaveTag(TagsModel model)
        {
            var tag = new Tags
            {
                Name = model.Name
            };

            _tagsRepository.Insert(tag);
        }

        public void UpdateTag(TagsModel model)
        {
            var tag = _tagsRepository.GetByID(model.TagId);
            tag.Name = model.Name;

            _tagsRepository.Update(tag);
        }

        public void DeleteTag(int id)
        {
            var artTagCollect = _articleTagsRepository.Get().Where(t => t.TagId == id);
            foreach (var articleTag in artTagCollect)
            {
                _articleTagsRepository.Delete(articleTag.ArticleId, articleTag.TagId);
            }
            _tagsRepository.Delete(id);
        }

        public TagsModel GetTag(int id)
        {
            var tag = _tagsRepository.GetByID(id);
            var model = new TagsModel
            {
                TagId = tag.TagId,
                Name = tag.Name
            };

            return model;
        }
    }
}
