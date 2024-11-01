using BTL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

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
            var user = await db.TaiKhoans
                .Select(t => new TaiKhoan
                {
                    MaTk = t.MaTk,
                    HoTen = t.HoTen ?? "Unknown",
                    Role = t.Role,
                    TenDangNhap = t.TenDangNhap,
                    MatKhau = t.MatKhau,
                    Email = t.Email ?? "N/A",
                    DienThoai = t.DienThoai ?? "N/A"
                }).SingleOrDefaultAsync(u => u.TenDangNhap == username && u.MatKhau == password);

            if (user != null)
            {
                // Thêm Claims để lưu MaTk và Role
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.TenDangNhap),
                    new Claim("MaTk", user.MaTk.ToString()),  // Lưu MaTk
                    new Claim(ClaimTypes.Role, user.Role.ToString()) // Lưu Role
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Đăng nhập duy trì
                };

                // Thiết lập cookie xác thực
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                if (user.Role == 1)
                {
                    return RedirectToAction("Index", "SanPhams");
                }
                else
                {
                    return RedirectToAction("Index", "HomeKH");
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
