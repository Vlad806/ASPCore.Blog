using System.Collections.Generic;

namespace ASPCore.Blog.Domain.Entities
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Articles> Articles { get; set; }
    }
}