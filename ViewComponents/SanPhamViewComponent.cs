using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.ViewComponents
{
    public class SanPhamViewComponent : ViewComponent
    {
        private readonly QlhieuThuocContext _context;

        public SanPhamViewComponent(QlhieuThuocContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Fetch products asynchronously and handle potential nulls
            var sanphams = await _context.SanPhams.ToListAsync();

            // Check if sanphams is null or empty
            if (sanphams == null || !sanphams.Any())
            {
                return View("RenderSanPham", new List<SanPham>()); // Return an empty list if no products
            }

            return View("RenderSanPham", sanphams);
        }
    }
}
