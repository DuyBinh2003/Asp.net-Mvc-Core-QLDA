using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly CContext _context;

        public HomeController(CContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Check if the UserId session variable is null
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                // UserId is null, redirect to the login page in the "Authorization" area
                return RedirectToAction("Login", "Account", new { area = "Authentication" });
            }

            var books = await _context.Books
                .Include(c => c.Category)
                .ToListAsync();
            ViewData["Books"] = books;

            return View();
        }

        public async Task<IActionResult> Menu()
        {
            var books = await _context.Books
                .Include(c => c.Category)
                .ToListAsync();
            return View(books);
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Product(int? id)
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
            var orderBooks = await _context.Books
                .Where(p => p.CategoryId == book.CategoryId)
                .Include(a => a.Author)
                .ToListAsync();
            ViewBag.Book = book;
            ViewBag.OtherBooks = orderBooks;
            return View();
        }
        public async Task<IActionResult> Order(int? id)
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
            return View(book);
        }
        public async Task<IActionResult> Cart(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }
            var carts = await _context.Carts
                .Where(p => p.UserId == id)
                .Include(b => b.Book)
                .ToListAsync();
            if (carts == null)
            {
                return NotFound();
            }
            return View(carts);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}