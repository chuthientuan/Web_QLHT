using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL.Controllers
{
    public class DonHangController : Controller
    {
        private readonly QlhieuThuocContext _context;
        public DonHangController(QlhieuThuocContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
