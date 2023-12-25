using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoAn.Filters;
using X.PagedList.Mvc.Core;
using X.PagedList;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorization]
    public class BooksController : Controller
    {
        private readonly CContext _context;
        private readonly INotyfService _notyf;

        public BooksController(CContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/Books
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Book> booksQuery = _context.Books
                .Include(s => s.InvoiceDetails)
                .Include(s => s.Author)
                .Include(s => s.Carts)
                .Include(s => s.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Name.Contains(searchString) ||
                    b.Author.Name.Contains(searchString) ||
                    b.Description.Contains(searchString) ||
                    b.BookId.ToString().Contains(searchString));
            }

            int pageNumber = page ?? 1;
            int pageSize = 5;

            var books = await booksQuery.ToPagedListAsync(pageNumber, pageSize);

            return View(books);
        }


        // GET: Admin/Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(s => s.InvoiceDetails)
                    .ThenInclude(i => i.Invoice)
                        .ThenInclude(i => i.User)
                 .Include(s => s.Author)
                 .Include(s => s.Carts)
                 .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Admin/Books/Create
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Name,Price,ImgPath,Description,AuthorId,CategoryId,Rate,Quantity")] Book book)
        {
            
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                _notyf.Success("Book created successful");
                return RedirectToAction(nameof(Index));
            }
           
            _notyf.Error("Book create unsuccessful");
            return View(book);
        }

        // GET: Admin/Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Name,Price,ImgPath,Description,AuthorId,CategoryId,Rate,Quantity")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notyf.Success("Book edited successful");
                return RedirectToAction(nameof(Index));
            }
            _notyf.Error("Book edit unsuccessful");
            return View(book);
        }

        // GET: Admin/Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'CContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            _notyf.Success("Book deleted successful");
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
