using DoAn.Filters;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorization]
    public class HomeController : Controller
    {
        private readonly CContext _context;

        public HomeController(CContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Calculate total amount of TotalPrice
            decimal totalAmount = await _context.Invoices.SumAsync(i => i.TotalPrice ?? 0);

            // Calculate total number of invoices
            int totalInvoices = await _context.Invoices.CountAsync();

            // Calculate total number of books
            int totalBooks = await _context.Books.CountAsync();

            // Calculate total number of users
            int totalUsers = await _context.Users.CountAsync();

            // Get the 5 most recent invoices
            var recentInvoices = await _context.Invoices
                .OrderByDescending(i => i.Date)
                .Take(5)
                .ToListAsync();
            // Get Invoice list
            var invoices = await _context.Invoices.ToListAsync();
            // Fetch books with categories using a join
            var books = await _context.Books.Include(b => b.Category).ToListAsync();

            var categoryCounts = books
                .GroupBy(b => b.CategoryId)
                .Select(group => new
                {
                    CategoryId = group.Key,
                    BookCount = group.Count()
                })
                .Join(
                    _context.Categories,
                    bookGroup => bookGroup.CategoryId,
                    category => category.CategoryId,
                    (bookGroup, category) => new
                    {
                        CategoryName = category.Name,
                        BookCount = bookGroup.BookCount
                    }
                )
                .ToList();

            ViewBag.CategoryCounts = categoryCounts;
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalInvoices = totalInvoices;
            ViewBag.Books = totalBooks;
            ViewBag.Users = totalUsers;
            ViewBag.RecentInvoices = recentInvoices;
            ViewBag.Invoices = invoices;
            ViewBag.UniqueYears = invoices.Select(i => i.Date.Year).Distinct().OrderByDescending(y => y).ToList();

            return View();
        }
    }
}
