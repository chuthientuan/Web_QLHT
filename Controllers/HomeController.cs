using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: /Account/DangNhap
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Replace this with your user validation logic
            bool isValidUser = ValidateUser(username, password);

            if (isValidUser)
            {
                // Redirect to the product index page on successful login
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError(string.Empty, "Tên tài kho?n ho?c m?t kh?u không ?úng.");
            return View();
        }
        // Simulated user validation (replace with actual authentication logic)
        private bool ValidateUser(string username, string password)
        {
            // Here, you would typically check the user credentials against a database
            return username == "admin" && password == "123"; // Example credentials
        }

    }
}
