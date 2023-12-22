using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        private readonly CContext _context;

        public CartController(CContext context)
        {  
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || _context.Carts == null)
            {
                return NotFound();
            }
            var carts = await _context.Carts
                .Where(p => p.UserId == int.Parse(userId.ToString()))
                .Include(b => b.Book)
                .ToListAsync();
            if (carts == null)
            {
                return NotFound();
            }
            return View(carts);
        }

        [HttpPost]
        public IActionResult RemoveItem(int bookId, int userId)
        {
            // Tìm mục giỏ hàng cần xóa
            var cartItem = _context.Carts.FirstOrDefault(item => item.BookId == bookId && item.UserId == userId);

            if (cartItem != null)
            {
                // Xóa mục giỏ hàng
                _context.Carts.Remove(cartItem);
                _context.SaveChanges();
            }

            // Chuyển hướng về trang giỏ hàng hoặc trang khác tùy thuộc vào logic của bạn
            return RedirectToAction("Index", "Cart");
        }
    }
}
