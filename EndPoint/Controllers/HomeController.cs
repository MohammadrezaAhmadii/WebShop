using Application.Interfaces.Context;
using Application.Services.Products.Queries.GetProductSiteById;
using Microsoft.AspNetCore.Mvc;

namespace testWebsit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataBaseContext _context;
        private readonly IGetProductSiteById _siteById;
        public HomeController(IDataBaseContext context, IGetProductSiteById siteById)
        {
            _context = context;
            _siteById = siteById;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetProduct(long Id)
        {
            return Json(_siteById.resultDto(Id).Date);
        }
    }
}
