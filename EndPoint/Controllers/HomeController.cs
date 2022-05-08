using Microsoft.AspNetCore.Mvc;

namespace testWebsit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
