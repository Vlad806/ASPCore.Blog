namespace ASPCore.Blog.Domain.Entities
{
    public class ArticleTags
    {
        public int ArticleId { get; set; }
        public int TagId { get; set; }
        public virtual Articles Article { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
