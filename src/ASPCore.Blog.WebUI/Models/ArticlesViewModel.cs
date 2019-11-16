using System.Collections.Generic;

namespace ASPCore.Blog.WebUI.Models
{
    public class ArticlesViewModel
    {
        public IEnumerable<ArticlesModel> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
