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
        public async Task<IActionResult> Index(int maHdb)
        {
            ViewBag.CurrentMaHdb = maHdb; // Giữ lại giá trị MaHdb để sử dụng trong view
            var chiTietHdbList = await _context.ChiTietHdbs
                .Include(c => c.MaHdbNavigation)
                .Include(c => c.MaSpNavigation)
                .Where(c => c.MaHdb == maHdb) // Lọc theo MaHdb
                .ToListAsync();
            return View(chiTietHdbList);
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
        public IActionResult Create(int maHdb)
        {
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "TenSp", "MaSp");
            var chiTietHdb = new ChiTietHdb { MaHdb = maHdb };
            return View(chiTietHdb);
        }


        // POST: ChiTietHdbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHdb,MaSp,Slban")] ChiTietHdb chiTietHdb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { maHdb = chiTietHdb.MaHdb }); // Redirect với MaHDB
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "TenSp", "MaSp", chiTietHdb.MaSp);
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
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "TenSp", "MaSp", chiTietHdb.MaSp);
            return View(chiTietHdb);
        }

        // POST: ChiTietHdbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHdb,MaSp,Slban")] ChiTietHdb chiTietHdb)
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
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "TenSp", "MaSp", chiTietHdb.MaSp);
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
