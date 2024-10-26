using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
