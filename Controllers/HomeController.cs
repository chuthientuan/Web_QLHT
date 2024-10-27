using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QlhieuThuocContext db;

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

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            var user = await db.TaiKhoans.SingleOrDefaultAsync(
                u => u.TenDangNhap == username && u.MatKhau == password);
            if (user != null)
            {
                if (user.Role == 1)
                {
                    return RedirectToAction("Index", "SanPhams");
                }
                else 
                {
                    return RedirectToAction("Index", "HoaDonBans");
                }
            }
            ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
            return View();
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangKy(string hoTen, string username, string email, string password, string confirmPassword, string? dienThoai)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                ModelState.AddModelError("hoTen", "Họ tên không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("username", "Tên tài khoản không được để trống.");
            }

            if (password.Length < 6)
            {
                ModelState.AddModelError("password", "Mật khẩu phải có ít nhất 6 ký tự.");
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Mật khẩu và xác nhận không khớp.");
            }

            if (ModelState.IsValid && await RegisterUserAsync(hoTen, username, email, password, dienThoai))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Đăng ký không thành công.");
            return View();
        }

        private async Task<bool> RegisterUserAsync(string hoTen, string username, string email, string password, string? dienThoai)
        {
            if (await db.TaiKhoans.AnyAsync(u => u.TenDangNhap == username))
            {
                ModelState.AddModelError(string.Empty, "Tên tài khoản đã tồn tại.");
                return false;
            }

            var newUser = new TaiKhoan
            {
                HoTen = hoTen,
                TenDangNhap = username,
                MatKhau = password,
                Email = email,
                DienThoai = dienThoai,
                Role = 0 // Default role
            };

            db.TaiKhoans.Add(newUser);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
