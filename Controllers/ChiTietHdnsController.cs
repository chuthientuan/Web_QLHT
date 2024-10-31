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
        public async Task<IActionResult> Index(int maHdn)
        {
            var chiTietHdns = await _context.ChiTietHdns
                .Include(c => c.MaSpNavigation) // Include product details if necessary
                .Where(c => c.MaHdn == maHdn) // Filter by the provided maHdn
                .ToListAsync();

            ViewBag.MaHdn = maHdn; // Make maHdn available to the view
            return View(chiTietHdns); // Return the list of details
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
        public IActionResult Create(int maHdn)
        {
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "TenSp");
            var chiTietHdn = new ChiTietHdn { MaHdn = maHdn }; // Initialize with the provided maHdn
            return View(chiTietHdn);
        }

        // POST: ChiTietHdns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdn,MaSp,Slnhap")] ChiTietHdn chiTietHdn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHdn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { maHdn = chiTietHdn.MaHdn });
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "TenSp", chiTietHdn.MaSp);
            return View(chiTietHdn);
        }

        // GET: ChiTietHdns/Edit/5
        public async Task<IActionResult> Edit(int maHdn, int maSp)
        {
            var chiTietHdn = await _context.ChiTietHdns
                .Include(c => c.MaHdnNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == maHdn && m.MaSp == maSp);

            if (chiTietHdn == null)
            {
                return NotFound();
            }

            return View(chiTietHdn);
        }

        // POST: ChiTietHdns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maHdn, int maSp, ChiTietHdn chiTietHdn)
        {
            if (maHdn != chiTietHdn.MaHdn || maSp != chiTietHdn.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHdn);
                    await _context.SaveChangesAsync();
                    // Redirect to Index with the maHdb parameter
                    return RedirectToAction(nameof(Index), new { maHdn = chiTietHdn.MaHdn });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHdnExists(maHdn, maSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(chiTietHdn);
        }
        private bool ChiTietHdnExists(int maHdn, int maSp)
        {
            return _context.ChiTietHdns.Any(e => e.MaHdn == maHdn && e.MaSp == maSp);
        }

        // GET: ChiTietHdns/Delete/maHdn/maSp
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
        public async Task<IActionResult> DeleteConfirmed(int maHdn, int maSp)
        {
            var chiTietHdn = await _context.ChiTietHdns
                .Include(c => c.MaHdnNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdn == maHdn && m.MaSp == maSp);

            if (chiTietHdn != null)
            {
                _context.ChiTietHdns.Remove(chiTietHdn);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ChiTietHdnExists(int id)
        {
            return _context.ChiTietHdns.Any(e => e.MaHdn == id);
        }
    }
}
