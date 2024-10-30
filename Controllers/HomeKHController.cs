using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    public class HomeKHController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
