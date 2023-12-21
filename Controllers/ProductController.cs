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

        [HttpPost]
        public IActionResult AddReview(string content, int rating, int bookId)
        {
            var newReview = new Review
            {
                Content = content,
                Rate = rating,
                BookId = bookId,
                UserId = 1
            };

            _context.Reviews.Add(newReview);
            _context.SaveChanges();

            return RedirectToAction("Detail", "Product", new { id = bookId });
        }
    }
}
