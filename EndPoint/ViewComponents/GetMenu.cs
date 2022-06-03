using Application.Services.Categories.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IGetMenuItem _getMenuItem;
        public GetMenu(IGetMenuItem getMenuItem)
        {
            _getMenuItem = getMenuItem;
        }
        public IViewComponentResult Invoke()
        {
            var menuItem = _getMenuItem.ResultDto();
            return View(viewName: "GetMenu", menuItem.Date);
        }
    }
}
