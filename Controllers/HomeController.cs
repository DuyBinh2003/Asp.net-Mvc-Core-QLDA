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
            var books = await _context.Books.ToListAsync();
            ViewData["Books"] = books;

            return View();
        }

        public async Task<IActionResult> Menu()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Product(int? id)
        {
            if (id == null || _context.Authors == null)
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
                .Where(p => p.CategoryId == book.CategoryId).ToListAsync();
            ViewBag.Book = book;
            ViewBag.OtherBooks = orderBooks;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}