using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    public class HistoryController : Controller
    {
        private readonly CContext _context;

        public HistoryController(CContext context)
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
            var invoices = await _context.Invoices
                .Where(p => p.UserId == int.Parse(userId.ToString()))
                .Include(i => i.InvoiceDetails)
                .ThenInclude(b => b.Book)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
            if (invoices == null)
            {
                return NotFound();
            }
            ViewBag.Invoice = invoices;
            return View();
        }
        public async Task<IActionResult> Order(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (_context.Books == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices
                .Where(i => i.InvoiceId == id)
                .Include(i => i.InvoiceDetails)
                .ThenInclude(id => id.Book)
                .FirstOrDefaultAsync();
            if (invoice == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == int.Parse(userId.ToString()));
            ViewBag.Invoice = invoice;
            ViewBag.User = user;
            return View();
        }
    }
}
