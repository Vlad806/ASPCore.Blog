using System.Collections.Generic;
using ASPCore.Blog.Domain.Entities;
using ASPCore.Blog.Domain.Repositories;
using ASPCore.Blog.WebUI.Models;

namespace ASPCore.Blog.WebUI.Services
{
    public class TagsService : ITagsService
    {
        private readonly IApplicationRepository<Tags> _tagsRepository;

        public TagsService(IApplicationRepository<Tags> tagsRepository)
        {
            _tagsRepository = tagsRepository;
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
    }
}
