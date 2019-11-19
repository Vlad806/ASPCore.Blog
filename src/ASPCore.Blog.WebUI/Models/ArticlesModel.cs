using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ASPCore.Blog.Domain.Entities;

namespace ASPCore.Blog.WebUI.Models
{
    public class ArticlesModel
    {
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "The article name field is required.")]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "The article text is required.")]
        [StringLength(10000)]
        public string Description { get; set; }
        public DateTime DateChange { get; set; }
        public byte[] HeroImage { get; set; }
        public CategoriesModel Category { get; set; }
        public IEnumerable<Tags> Tags { get; set; }
        public IEnumerable<int> SelectedTags { get; set; }
    }
}
