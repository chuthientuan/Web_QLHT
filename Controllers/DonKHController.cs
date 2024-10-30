using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    public class DonKHController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
