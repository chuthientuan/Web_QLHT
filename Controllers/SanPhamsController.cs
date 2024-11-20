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
    public class SanPhamsController : Controller
    {
        private readonly QlhieuThuocContext _context;
        private int pageSize = 10;
        public SanPhamsController(QlhieuThuocContext context)
        {
            _context = context;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index(int page = 1, int? loaiThuocId = null)
        {
            var query = _context.SanPhams.Include(s => s.MaLtNavigation).AsQueryable();

            // Filter by LoaiThuoc if specified
            if (loaiThuocId.HasValue)
            {
                query = query.Where(p => p.MaLt == loaiThuocId.Value);
            }

            // Pagination logic
            var totalProducts = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Pass current page, total pages, and filter data to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.LoaiThuocId = loaiThuocId;

            // Pass list of LoaiThuoc for the filter dropdown
            ViewBag.LoaiThuocs = await _context.LoaiThuocs.ToListAsync();

            return View(products);
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLtNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "image_products");
            var images = Directory.GetFiles(imagesPath).Select(Path.GetFileName).ToList();

            // Pass the list of images to the view
            ViewBag.Anh = new SelectList(images);

            // Populate LoaiThuocs as before
            ViewBag.MaLt = new SelectList(_context.LoaiThuocs, "MaLt", "TenLt");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,MaLt,TenSp,DonGiaNhap,MoTa,DonGiaBan,SoLuong,Anh,Hsd")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                // Set the image path (you may adjust this path as needed)
                sanPham.Anh = Path.Combine("images/image_products", sanPham.Anh);

                _context.Add(sanPham);
                await _context.SaveChangesAsync(); // Make sure to await this call
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the dropdowns if the model state is invalid
            ViewBag.MaLt = new SelectList(_context.LoaiThuocs, "MaLt", "TenLt", sanPham.MaLt);
            ViewBag.Anh = new SelectList(Directory.GetFiles("wwwroot/images/image_products").Select(Path.GetFileName).ToList());

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "image_products");
            var images = Directory.GetFiles(imagesPath).Select(Path.GetFileName).ToList();
            ViewBag.Anh = new SelectList(images, sanPham.Anh);

            ViewBag.MaLtNavigation = new SelectList(_context.LoaiThuocs, "MaLt", "TenLt", sanPham.MaLt);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenLt,MaLt,TenSp,DonGiaNhap,MoTa,DonGiaBan,SoLuong,Anh,Hsd")] SanPham sanPham)
        {
            if (id != sanPham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sanPham.Anh = Path.Combine("images/image_products", sanPham.Anh);
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSp))
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
            ViewData["MaLt"] = new SelectList(_context.LoaiThuocs, "MaLt", "TenLt", sanPham.MaLt);
            ViewBag.Anh = new SelectList(Directory.GetFiles("wwwroot/images/image_products").Select(Path.GetFileName).ToList(), sanPham.Anh);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLtNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
    }
}
