using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        private readonly CContext _context;
        private readonly INotyfService _notyf;

        public CartController(CContext context, INotyfService notyf)
        {  
            _context = context;
            _notyf = notyf;
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
            ViewBag.Cart = carts;
            return View();
        }
        public async Task<IActionResult> Order()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (_context.Books == null)
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
            decimal totalPrice = carts.Sum(item => (decimal)item.Book.Price * item.Quantity);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == int.Parse(userId.ToString()));
            ViewBag.Cart = carts;
            ViewBag.User = user;
            ViewBag.TotalPrice = totalPrice;
            return View();
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
                _notyf.Success("Book removed successful");
            }

            // Chuyển hướng về trang giỏ hàng hoặc trang khác tùy thuộc vào logic của bạn
            return RedirectToAction("Index", "Cart");
        }
        
        [HttpPost]
        public IActionResult UpdateQuantity(int bookId, int userId, int newQuantity)
        {
            var cartItem = _context.Carts.FirstOrDefault(item => item.BookId == bookId && item.UserId == userId);

            if (cartItem != null && newQuantity > 0)
            {
                cartItem.Quantity = newQuantity;
                _context.SaveChanges();
                _notyf.Success("Quantity updated successful");
            }

            // Chuyển hướng về trang giỏ hàng hoặc trang khác tùy thuộc vào logic của bạn
            return RedirectToAction("Index", "Cart");
        }
    }
}
