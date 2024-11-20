using System.Security.Claims;
using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers
{
    public class DonKHController : Controller
    {
        private readonly QlhieuThuocContext _context;
        public DonKHController(QlhieuThuocContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Assuming you store MaTk in claims or session; adjust as needed
            var maTkClaim = User.FindFirst("MaTk")?.Value; // Get MaTk from claims
            if (int.TryParse(maTkClaim, out int maTk))
            {
                // Fetch sales orders where MaTk matches the logged-in user's MaTk
                var hoaDonBans = await _context.HoaDonBans
                    .Where(h => h.MaTk == maTk)
                    .ToListAsync();

                return View(hoaDonBans);
            }

            return NotFound(); // Or handle the case where MaTk is not found
        }


    }
}
