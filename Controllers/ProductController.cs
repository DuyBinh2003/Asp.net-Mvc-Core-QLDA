using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DoAn.Controllers
{
    public class ProductController : Controller
    {
        private readonly CContext _context;

        public ProductController(CContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            var totalSoldQuantity = _context.InvoiceDetails
                .Where(d => d.BookId == book.BookId)
                .Sum(d => d.Quantity);

            var orderBooks = await _context.Books
                .Where(p => p.CategoryId == book.CategoryId)
                .Union(_context.Books.Where(b => b.AuthorId == book.AuthorId))
                .Where(p => p.BookId != book.BookId)
                .Include(a => a.Author)
                .ToListAsync();

            var comments = await _context.Reviews
                .Where(r => r.BookId == book.BookId)
                .Include(u => u.User)
                .ToListAsync();

            ViewBag.Book = book;
            ViewBag.OtherBooks = orderBooks;
            ViewBag.totalSoldQuantity = totalSoldQuantity;
            ViewBag.Comments = comments;
            return View();
        }

        public async Task<IActionResult> Order(int id, int? quantity)
        {
            if (quantity == null) quantity = 1;  
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                .Include(a => a.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == int.Parse(userId.ToString()));
            ViewBag.Book = book;
            ViewBag.User = user;
            ViewBag.Quantity= quantity;
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(string content, int rating, int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var newReview = new Review
            {
                Content = content,
                Rate = rating,
                BookId = bookId,
                UserId = int.Parse(userId.ToString())
            };

            _context.Reviews.Add(newReview);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Product", new { id = bookId });
        }
        [HttpPost]
        public IActionResult AddCart(int quantity, int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var existingCartItem = _context.Carts
            .FirstOrDefault(item => item.BookId == bookId && item.UserId == int.Parse(userId.ToString()));

            if (existingCartItem == null)
            {
                // Nếu chưa có, thêm một dòng mới
                var newCartItem = new Cart
                {
                    BookId = bookId,
                    UserId = int.Parse(userId.ToString()),
                    Quantity = quantity
                };

                _context.Carts.Add(newCartItem);
            }
            else
            {
                // Nếu đã tồn tại, cập nhật quantity
                existingCartItem.Quantity += quantity;
                _context.Carts.Update(existingCartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Detail", "Product", new { id = bookId });
        }
    }
}
