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
    public class ChiTietHdbsController : Controller
    {
        private readonly QlhieuThuocContext _context;

        public ChiTietHdbsController(QlhieuThuocContext context)
        {
            _context = context;
        }

        // GET: ChiTietHdbs
        public async Task<IActionResult> Index()
        {
            var qlhieuThuocContext = _context.ChiTietHdbs.Include(c => c.MaHdbNavigation).Include(c => c.MaSpNavigation);
            return View(await qlhieuThuocContext.ToListAsync());
        }

        // GET: ChiTietHdbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdb = await _context.ChiTietHdbs
                .Include(c => c.MaHdbNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == id);
            if (chiTietHdb == null)
            {
                return NotFound();
            }

            return View(chiTietHdb);
        }

        // GET: ChiTietHdbs/Create
        public IActionResult Create()
        {
            ViewData["MaHdb"] = new SelectList(_context.HoaDonBans, "MaHdb", "MaHdb");
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp");
            return View();
        }

        // POST: ChiTietHdbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdb,MaSp,Slban,KhuyenMai")] ChiTietHdb chiTietHdb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHdb"] = new SelectList(_context.HoaDonBans, "MaHdb", "MaHdb", chiTietHdb.MaHdb);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdb.MaSp);
            return View(chiTietHdb);
        }

        // GET: ChiTietHdbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdb = await _context.ChiTietHdbs.FindAsync(id);
            if (chiTietHdb == null)
            {
                return NotFound();
            }
            ViewData["MaHdb"] = new SelectList(_context.HoaDonBans, "MaHdb", "MaHdb", chiTietHdb.MaHdb);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdb.MaSp);
            return View(chiTietHdb);
        }

        // POST: ChiTietHdbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHdb,MaSp,Slban,KhuyenMai")] ChiTietHdb chiTietHdb)
        {
            if (id != chiTietHdb.MaHdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHdb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHdbExists(chiTietHdb.MaHdb))
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
            ViewData["MaHdb"] = new SelectList(_context.HoaDonBans, "MaHdb", "MaHdb", chiTietHdb.MaHdb);
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietHdb.MaSp);
            return View(chiTietHdb);
        }

        // GET: ChiTietHdbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHdb = await _context.ChiTietHdbs
                .Include(c => c.MaHdbNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == id);
            if (chiTietHdb == null)
            {
                return NotFound();
            }

            return View(chiTietHdb);
        }

        // POST: ChiTietHdbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHdb = await _context.ChiTietHdbs.FindAsync(id);
            if (chiTietHdb != null)
            {
                _context.ChiTietHdbs.Remove(chiTietHdb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHdbExists(int id)
        {
            return _context.ChiTietHdbs.Any(e => e.MaHdb == id);
        }
    }
}
