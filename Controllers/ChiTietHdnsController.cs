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
    public class ChiTietHdnsController : Controller
    {
        private readonly QlhieuThuocContext _context;

        public ChiTietHdnsController(QlhieuThuocContext context)
        {
            _context = context;
        }

        // GET: ChiTietHdns
        public async Task<IActionResult> Index()
        {
            var qlhieuThuocContext = _context.ChiTietHdns.Include(c => c.MaHdnNavigation).Include(c => c.MaSpNavigation);
            return View(await qlhieuThuocContext.ToListAsync());
        }

        // GET: ChiTietHdns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdn = await _context.ChiTietHdns
                .Include(c => c.MaHdnNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == id);
            if (chiTietHdn == null)
            {
                return NotFound();
            }

            return View(chiTietHdn);
        }

        // GET: ChiTietHdns/Create
        public IActionResult Create()
        {
            ViewData["MaHdn"] = new SelectList(_context.HoaDonNhaps, "MaHdn", "MaHdn");
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp");
            return View();
        }

        // POST: ChiTietHdns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdn,MaSp,Slnhap,KhuyenMai")] ChiTietHdn chiTietHdn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHdn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHdn"] = new SelectList(_context.HoaDonNhaps, "MaHdn", "MaHdn", chiTietHdn.MaHdn);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdn.MaSp);
            return View(chiTietHdn);
        }

        // GET: ChiTietHdns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdn = await _context.ChiTietHdns.FindAsync(id);
            if (chiTietHdn == null)
            {
                return NotFound();
            }
            ViewData["MaHdn"] = new SelectList(_context.HoaDonNhaps, "MaHdn", "MaHdn", chiTietHdn.MaHdn);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdn.MaSp);
            return View(chiTietHdn);
        }

        // POST: ChiTietHdns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHdn,MaSp,Slnhap,KhuyenMai")] ChiTietHdn chiTietHdn)
        {
            if (id != chiTietHdn.MaHdn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHdn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHdnExists(chiTietHdn.MaHdn))
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
            ViewData["MaHdn"] = new SelectList(_context.HoaDonNhaps, "MaHdn", "MaHdn", chiTietHdn.MaHdn);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdn.MaSp);
            return View(chiTietHdn);
        }

        // GET: ChiTietHdns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdn = await _context.ChiTietHdns
                .Include(c => c.MaHdnNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == id);
            if (chiTietHdn == null)
            {
                return NotFound();
            }

            return View(chiTietHdn);
        }

        // POST: ChiTietHdns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHdn = await _context.ChiTietHdns.FindAsync(id);
            if (chiTietHdn != null)
            {
                _context.ChiTietHdns.Remove(chiTietHdn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHdnExists(int id)
        {
            return _context.ChiTietHdns.Any(e => e.MaHdn == id);
        }
    }
}
