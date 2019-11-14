using System.Collections.Generic;
using ASPCore.Blog.Domain.Entities;

namespace ASPCore.Blog.WebUI.Models
{
    public class ArticlesModel
    {
        public IEnumerable<Articles> Articles { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Tags> Tags { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
