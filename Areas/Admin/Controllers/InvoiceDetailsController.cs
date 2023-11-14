using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn.Models;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceDetailsController : Controller
    {
        private readonly CContext _context;

        public InvoiceDetailsController(CContext context)
        {
            _context = context;
        }

        // GET: Admin/InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var cContext = _context.InvoiceDetails.Include(i => i.Book).Include(i => i.Invoice);
            return View(await cContext.ToListAsync());
        }

        // GET: Admin/InvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.Book)
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId");
            return View();
        }

        // POST: Admin/InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,BookId,UnitPrice,Quantity")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", invoiceDetail.InvoiceId);
            return View(invoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", invoiceDetail.InvoiceId);
            return View(invoiceDetail);
        }

        // POST: Admin/InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,BookId,UnitPrice,Quantity")] InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailExists(invoiceDetail.InvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", invoiceDetail.BookId);
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", invoiceDetail.InvoiceId);
            return View(invoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.Book)
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // POST: Admin/InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'CContext.InvoiceDetails'  is null.");
            }
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(int id)
        {
          return (_context.InvoiceDetails?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
