using BTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Controllers
{
    public class CartController : Controller
    {
        QlhieuThuocContext _context;
        public CartController(QlhieuThuocContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart(int productId)
        {
            // Retrieve existing cart from session, or create a new one if none exists
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Find the product in the database
            var product = _context.SanPhams.FirstOrDefault(p => p.MaSp == productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Check if the product is already in the cart
            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++; // Increase quantity if already in cart
            }
            else
            {
                // Add new item to the cart if it does not exist
                cart.Add(new CartItem
                {
                    ProductId = product.MaSp,
                    ProductName = product.TenSp,
                    Price = product.DonGiaBan,
                    Quantity = 1,
                    ImageUrl = product.Anh
                });
            }

            // Save updated cart back to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult Remove(int productId)
        {
            // Lấy giỏ hàng từ session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Tìm sản phẩm trong giỏ hàng
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove); // Xóa sản phẩm khỏi giỏ hàng
            }

            // Lưu giỏ hàng đã cập nhật lại vào session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }


    }
}
