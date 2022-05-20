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
        public IActionResult Index(string searchkey,int page = 1, long? Id = null)
        {
            return View(_facadPattern.GetProductForSite.ResultDto(searchkey, page,Id).Date);
        }
        public IActionResult Detail(long Id)
        {
            return View(_facadPattern.GetProductSiteById.resultDto(Id).Date);
        }
    }
}
