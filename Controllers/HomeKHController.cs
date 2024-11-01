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
    }
}
