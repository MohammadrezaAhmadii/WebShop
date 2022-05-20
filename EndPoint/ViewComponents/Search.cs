using Application.Services.Categories.Queries.GetAllCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly IGetAllCategory _getCategoryService;
        public Search(IGetAllCategory getCategoryService)
        {
            _getCategoryService = getCategoryService;
        }


        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _getCategoryService.Result().Date);
        }
    }
}
