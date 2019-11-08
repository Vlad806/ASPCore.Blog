using System.Collections.Generic;

namespace ASPCore.Blog.Domain.Entities
{
    public class Tags
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ArticleTags> ArticleTags { get; set; }
    }
}