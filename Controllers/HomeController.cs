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
            ModelState.AddModelError(string.Empty, "Wrong username or password.");
            return View();
        }

        // Simulated user validation (replace with actual authentication logic)
        private bool ValidateUser(string username, string password)
        {
            // Here, you would typically check the user credentials against a database
            return username == "admin" && password == "123"; // Example credentials
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: /Account/DangKy
        [HttpPost]
        public IActionResult DangKy(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Wrong password.");
                return View();
            }

            // Simulate user registration logic (replace with actual registration logic)
            if (RegisterUser(username, email, password))
            {
                // Redirect to the login page on successful registration
                return RedirectToAction("DangNhap");
            }

            ModelState.AddModelError(string.Empty, "Sign up error.");
            return View();
        }

        // Simulated user registration logic (replace with actual registration logic)
        private bool RegisterUser(string username, string email, string password)
        {
            // Here, you would typically save the user credentials to a database
            return true; // Example successful registration
        }
    }
}
