using System;

namespace ASPCore.Blog.WebUI.Models
{
    public class ArticlesModel
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime DateChange { get; set; }
        public byte[] HeroImage { get; set; }
        public CategoriesModel Category { get; set; }
    }
}
