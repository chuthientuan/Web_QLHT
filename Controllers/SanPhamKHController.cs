using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    public class SanPhamKHController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DuocPham()
        {
            return View();
        }
    }
}
