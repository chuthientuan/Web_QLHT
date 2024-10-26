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
    public class HoaDonNhapsController : Controller
    {
        private readonly QlhieuThuocContext _context;

        public HoaDonNhapsController(QlhieuThuocContext context)
        {
            _context = context;
        }

        // GET: HoaDonNhaps
        public async Task<IActionResult> Index()
        {
            var qlhieuThuocContext = _context.HoaDonNhaps.Include(h => h.MaNccNavigation);
            return View(await qlhieuThuocContext.ToListAsync());
        }

        // GET: HoaDonNhaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonNhap = await _context.HoaDonNhaps
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            return View(hoaDonNhap);
        }

        // GET: HoaDonNhaps/Create
        public IActionResult Create()
        {
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc");
            return View();
        }

        // POST: HoaDonNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdn,NgayNhap,MaNcc")] HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDonNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hoaDonNhap.MaNcc);
            return View(hoaDonNhap);
        }

        // GET: HoaDonNhaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonNhap = await _context.HoaDonNhaps.FindAsync(id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hoaDonNhap.MaNcc);
            return View(hoaDonNhap);
        }

        // POST: HoaDonNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHdn,NgayNhap,MaNcc")] HoaDonNhap hoaDonNhap)
        {
            if (id != hoaDonNhap.MaHdn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDonNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonNhapExists(hoaDonNhap.MaHdn))
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
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "MaNcc", hoaDonNhap.MaNcc);
            return View(hoaDonNhap);
        }

        // GET: HoaDonNhaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDonNhap = await _context.HoaDonNhaps
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            return View(hoaDonNhap);
        }

        // POST: HoaDonNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDonNhap = await _context.HoaDonNhaps.FindAsync(id);
            if (hoaDonNhap != null)
            {
                _context.HoaDonNhaps.Remove(hoaDonNhap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonNhapExists(int id)
        {
            return _context.HoaDonNhaps.Any(e => e.MaHdn == id);
        }
    }
}
