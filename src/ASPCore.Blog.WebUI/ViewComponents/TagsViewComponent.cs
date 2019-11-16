using System.Threading.Tasks;
using ASPCore.Blog.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ASPCore.Blog.WebUI.ViewComponents
{
    public class TagsViewComponent : ViewComponent
    {
        private readonly ITagsService _tagsService;

        public TagsViewComponent(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        public Task<ViewViewComponentResult> InvokeAsync()
        {
            return Task.FromResult(View(_tagsService.GetTagsModelCollection()));
        }
    }
}
