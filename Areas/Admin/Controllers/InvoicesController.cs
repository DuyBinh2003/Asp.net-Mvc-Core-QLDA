using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn.Models;
using DoAn.Filters;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorization]
    public class InvoicesController : Controller
    {
        private readonly CContext _context;

        public InvoicesController(CContext context)
        {
            _context = context;
        }

        // GET: Admin/Invoices
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Invoice> invoicesQuery = _context.Invoices
                .Include(s => s.InvoiceDetails)
                .Include(s => s.User);

            if (!string.IsNullOrEmpty(searchString))
            {
                invoicesQuery = invoicesQuery.Where(i => i.User.Name.Contains(searchString));
            }

            int pageNumber = page ?? 1;
            int pageSize = 5;

            var invoices = await invoicesQuery.ToPagedListAsync(pageNumber, pageSize);

            return View(invoices);
        }


        // GET: Admin/Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(u => u.User)
                .Include(s => s.InvoiceDetails)
                .ThenInclude(c => c.Book)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Admin/Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,UserId,Date,TotalPrice,Address,Note,Sdt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
