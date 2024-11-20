using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers
{
    public class HomeKHController : Controller
    {
        private readonly QlhieuThuocContext _db;

        public HomeKHController(QlhieuThuocContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IActionResult Index()
        {
            var products = _db.SanPhams.Include(c => c.MaLtNavigation).ToList();
            return View(products);
        }

        public IActionResult ThemVaoGioHang(int maSP)
        {
            // Lấy sản phẩm từ database
            var sanPham = _db.SanPhams.FirstOrDefault(sp => sp.MaSp == maSP);

            if (sanPham != null)
            {
                // Lấy giỏ hàng hiện tại từ Session
                var gioHang = HttpContext.Session.GetObjectFromJson<List<SanPham>>("GioHang") ?? new List<SanPham>();

                // Thêm sản phẩm vào giỏ hàng
                gioHang.Add(sanPham);

                // Lưu lại giỏ hàng vào Session
                HttpContext.Session.SetObjectAsJson("GioHang", gioHang);
            }

            // Chuyển hướng về trang Index hoặc Giỏ Hàng
            return RedirectToAction("Index");
        }
    }
}
