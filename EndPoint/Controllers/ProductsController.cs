using Application.Interfaces.FacadPattern;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IFacadPattern _facadPattern;
        public ProductsController(IFacadPattern facadPattern)
        {
            _facadPattern = facadPattern;
        }
        public IActionResult Index(string searchkey,long? catId = null,int page = 1)
        {
            return View(_facadPattern.GetAllProductForSite.Execute(searchkey,catId,page).Date);
        }
        public IActionResult Detail(long Id)
        {
            return View(_facadPattern.GetProductSiteById.resultDto(Id).Date);
        }
    }
}
