﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ASPCore.Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ASPCore.Blog.WebUI.ViewComponents
{
    public class TagsViewComponent : ViewComponent
    {
        public Task<ViewViewComponentResult> InvokeAsync(IEnumerable<Tags> listModels)
        {
            return Task.FromResult(View(listModels));
        }
    }
}