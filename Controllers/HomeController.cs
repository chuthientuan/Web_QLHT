using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QlhieuThuocContext db;

        public HomeController(ILogger<HomeController> logger, QlhieuThuocContext context)
        {
            _logger = logger;
            db = context;
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
            var user = await db.TaiKhoans.SingleOrDefaultAsync(
                    u => u.TenDangNhap == username && u.MatKhau == password);
            // Replace this with your user validation logic
            if (user != null)
            {
                if (user.Role == 1) // Assuming '1' represents admin
                {
                    return RedirectToAction("Index", "SanPhams");
                }
                else // Redirect to customer page if not admin
                {
                    return RedirectToAction("Index", "HoaDonBans");
                }
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
        public async Task<IActionResult> DangKy(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Password and confirmation do not match.");
                return View();
            }

            if (await RegisterUserAsync(username, email, password))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Sign up error.");
            return View();
        }

        // Simulated user registration logic (replace with actual registration logic)
        private async Task<bool> RegisterUserAsync(string username, string email, string password)
        {
            if (await db.TaiKhoans.AnyAsync(u => u.TenDangNhap == username || u.Email == email))
            {
                ModelState.AddModelError(string.Empty, "Username or email already exists.");
                return false;
            }

            var newUser = new TaiKhoan
            {
                HoTen = username, // Or however you'd like to set this
                TenDangNhap = username,
                MatKhau = password,
                Email = email,
                Role = 0 // Automatically set Role to 0
            };
            db.TaiKhoans.Add(newUser);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
