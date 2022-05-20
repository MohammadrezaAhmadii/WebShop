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
            return View(viewName: "GetMenu", _getMenuItem.ResultDto().Date);
        }
    }
}
