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
            var chiTietHdbs = await _context.ChiTietHdbs
                .Include(c => c.MaSpNavigation) // Include product details if necessary
                .Where(c => c.MaHdb == maHdb) // Filter by the provided maHdb
                .ToListAsync();

            ViewBag.MaHdb = maHdb; // Make maHdb available to the view
            return View(chiTietHdbs); // Return the list of details
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
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "TenSp");
            var chiTietHdb = new ChiTietHdb { MaHdb = maHdb }; // Initialize with the provided maHdb
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
                return RedirectToAction(nameof(Index), new { maHdb = chiTietHdb.MaHdb });
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "TenSp", chiTietHdb.MaSp);
            return View(chiTietHdb);
        }


        // GET: ChiTietHdb/Edit/5
        public async Task<IActionResult> Edit(int maHdb, int maSp)
        {
            var chiTietHdb = await _context.ChiTietHdbs
                .Include(c => c.MaHdbNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == maHdb && m.MaSp == maSp);

            if (chiTietHdb == null)
            {
                return NotFound();
            }

            return View(chiTietHdb);
        }

        // POST: ChiTietHdb/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maHdb, int maSp, ChiTietHdb chiTietHdb)
        {
            if (maHdb != chiTietHdb.MaHdb || maSp != chiTietHdb.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHdb);
                    await _context.SaveChangesAsync();
                    // Redirect to Index with the maHdb parameter
                    return RedirectToAction(nameof(Index), new { maHdb = chiTietHdb.MaHdb });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHdbExists(maHdb, maSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(chiTietHdb);
        }

        // This function checks if a ChiTietHdb exists based on the composite key
        private bool ChiTietHdbExists(int maHdb, int maSp)
        {
            return _context.ChiTietHdbs.Any(e => e.MaHdb == maHdb && e.MaSp == maSp);
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
        public async Task<IActionResult> DeleteConfirmed(int maHdb, int maSp)
        {
            var chiTietHdb = await _context.ChiTietHdbs
                .Include(c => c.MaHdbNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHdb == maHdb && m.MaSp == maSp);

            if (chiTietHdb != null)
            {
                _context.ChiTietHdbs.Remove(chiTietHdb);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ChiTietHdbExists(int id)
        {
            return _context.ChiTietHdbs.Any(e => e.MaHdb == id);
        }
    }
}
