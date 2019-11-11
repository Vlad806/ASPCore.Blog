using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using ASPCore.Blog.WebUI.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPCore.Blog.WebUI.HttpHandler
{
    public static class PagingHelpers
    {
        public static IHtmlContent PageLinks(this IHtmlHelper html,
            PagingInfo pagingInfo,
            string controller,
            string action)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tagLi = new TagBuilder("li");
                tagLi.AddCssClass("page-item");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("active");
                }

                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", $"\\{controller}\\{action}?page={i}");
                tag.InnerHtml.AppendHtml("0" + i);
                tag.AddCssClass("page-link");

                tagLi.InnerHtml.AppendHtml(tag);
                result.Append(GetString(tagLi));
            }

            return new HtmlString(result.ToString());
        }

        public static string GetString(IHtmlContent content)
        {
            var writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
