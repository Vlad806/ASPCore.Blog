using System.Collections.Generic;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCore.Blog.WebUI.Services
{
    public interface ITagsService
    {
        IEnumerable<TagsModel> GetTagsModelCollection();
        List<SelectListItem> GeTagsSelectedList(IEnumerable<int> selectedItemId = null);
        void SaveTag(TagsModel model);
        void UpdateTag(TagsModel model);
        void DeleteTag(int id);
        TagsModel GetTag(int id);
    }
}
