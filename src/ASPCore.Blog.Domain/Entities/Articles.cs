using System.Collections.Generic;

namespace ASPCore.Blog.Domain.Entities
{
    public class Articles
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public byte[] HeroImage { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }
        public virtual ICollection<ArticleTags> ArticleTags { get; set; }
    }
}