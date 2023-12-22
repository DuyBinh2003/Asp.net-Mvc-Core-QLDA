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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}