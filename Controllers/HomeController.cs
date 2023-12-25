using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {

        private readonly CContext _context;
        private readonly INotyfService _notyf;
        public HomeController(CContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index(int pageSizeIncrement = 6)
        {
            // Check if the UserId session variable is null
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                // UserId is null, redirect to the login page in the "Authorization" area
                return RedirectToAction("Login", "Account", new { area = "Authentication" });
            }

            var totalBooks = await _context.Books.CountAsync();
            var pageSize = pageSizeIncrement;

            var books = await _context.Books
                .Include(c => c.Category)
                .OrderBy(b => b.BookId)
                .Take(pageSize)
                .ToListAsync();

            ViewData["Books"] = books;
            ViewData["PageSizeIncrement"] = pageSizeIncrement;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalBooks"] = totalBooks;

            // Pass the scroll position to the view
            ViewData["ScrollPosition"] = HttpContext.Session.GetInt32("ScrollPosition") ?? 0;

            return View();
        }


        public async Task<IActionResult> Menu(int pageSizeIncrement = 6)
        {
            var totalBooks = await _context.Books.CountAsync();
            var pageSize = pageSizeIncrement;

            var books = await _context.Books
                .Include(c => c.Category)
                .OrderBy(b => b.BookId)
                .Take(pageSize)
                .ToListAsync();

            ViewData["Books"] = books;
            ViewData["PageSizeIncrement"] = pageSizeIncrement;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalBooks"] = totalBooks;

            // Pass the scroll position to the view
            ViewData["ScrollPosition"] = HttpContext.Session.GetInt32("ScrollPosition") ?? 0;

            return View(books);
        }

        // Add a new action to handle the search functionality
        public async Task<IActionResult> Search(string searchString, int pageSizeIncrement = 6)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                // Search string is null or empty
                _notyf.Information("Please enter a book name.");
                return RedirectToAction("Menu", new { pageSizeIncrement, scrollPosition = ViewData["ScrollPosition"] });
            }

            var totalBooks = await _context.Books.CountAsync();
            var pageSize = pageSizeIncrement;

            var books = await _context.Books
                .Include(c => c.Category)
                .Where(b => b.Name.Contains(searchString))
                .OrderBy(b => b.BookId)
                .Take(pageSize)
                .ToListAsync();

            if (books.Count == 0)
            {
                // No matching books found
                _notyf.Information("No matching books found.");
                return RedirectToAction("Menu", new { pageSizeIncrement, scrollPosition = ViewData["ScrollPosition"] });
            }

            ViewData["Books"] = books;
            ViewData["PageSizeIncrement"] = pageSizeIncrement;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalBooks"] = totalBooks;

            // Pass the scroll position to the view
            ViewData["ScrollPosition"] = HttpContext.Session.GetInt32("ScrollPosition") ?? 0;

            return View("Menu", books);
        }


        public IActionResult About()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Logout
        public IActionResult Logout()
        {
            // Clear user-related session values
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserType");

            // Redirect to the login page in the Authentication area
            _notyf.Success("Logout successful");
            return RedirectToAction("Login", "Account", new { area = "Authentication" });
        }
    }
}