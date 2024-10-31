using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL.Models;

namespace BTL.Controllers
{
    public class HoaDonBansController : Controller
    {
        private readonly QlhieuThuocContext _context;

        public HoaDonBansController(QlhieuThuocContext context)
        {
            _context = context;
        }

        // GET: HoaDonBans
        public async Task<IActionResult> Index()
        {
            var qlhieuThuocContext = _context.HoaDonBans
                .Include(h => h.MaTkNavigation)
                .Include(h => h.ChiTietHdbs)
                .ThenInclude(ct => ct.MaSpNavigation); 
            return View(await qlhieuThuocContext.ToListAsync());
        }

        // GET: HoaDonBans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans
                .Include(h => h.MaTkNavigation)
                .Include(h => h.ChiTietHdbs)
                .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }

            return View(hoaDonBan);
        }

        // GET: HoaDonBans/Create
        public IActionResult Create()
        {
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "HoTen");
            ViewData["SanPhamList"] = new SelectList(_context.SanPhams, "MaSp", "TenSp");
            return View();
        }

        // POST: HoaDonBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdb,NgayBan,TrangThai,MaTk")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDonBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "HoTen", hoaDonBan.MaTk);
            ViewData["SanPhamList"] = new SelectList(_context.SanPhams, "MaSp", "TenSp");
            return View(hoaDonBan);
        }

        // GET: HoaDonBans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans
                            .Include(h => h.MaTkNavigation) 
                            .Include(h => h.ChiTietHdbs)
                            .ThenInclude(ct => ct.MaSpNavigation) 
                            .FirstOrDefaultAsync(m => m.MaHdb == id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "HoTen", hoaDonBan.MaTk);
            ViewData["TenSp"] = hoaDonBan.ChiTietHdbs.FirstOrDefault()?.MaSpNavigation?.TenSp;
            return View(hoaDonBan);
        }

        // POST: HoaDonBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHdb,MaSp,NgayBan,TrangThai,MaTk")] HoaDonBan hoaDonBan)
        {
            if (id != hoaDonBan.MaHdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDonBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonBanExists(hoaDonBan.MaHdb))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTk"] = new SelectList(_context.TaiKhoans, "MaTk", "MaTk", hoaDonBan.MaTk);
            return View(hoaDonBan);
        }

        // GET: HoaDonBans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonBan = await _context.HoaDonBans
                .Include(h => h.MaTkNavigation)
                .Include(h => h.ChiTietHdbs)
                .ThenInclude(ct => ct.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }

            return View(hoaDonBan);
        }

        // POST: HoaDonBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDonBan = await _context.HoaDonBans.FindAsync(id);
            if (hoaDonBan != null)
            {
                _context.HoaDonBans.Remove(hoaDonBan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonBanExists(int id)
        {
            return _context.HoaDonBans.Any(e => e.MaHdb == id);
        }
    }
}
