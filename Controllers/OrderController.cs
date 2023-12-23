using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DoAn.Controllers
{
    public class OrderController : Controller
    {
        private readonly CContext _context;

        public OrderController(CContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult BuyCart(int userId, string sdt, string address, string note)
        {
            var newInvoice = new Invoice
            {
                UserId = userId,
                Sdt = sdt,
                Address = address,
                Note = note,
                Date = DateTime.Now,
            };

            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();

            var invoiceId = _context.Invoices.Max(i => i.InvoiceId);
            var cartItems = _context.Carts
                .Where(item => item.UserId == userId)
                .Include(b => b.Book)
                .ToList();

            foreach (var cartItem in cartItems)
            {
                var newInvoiceDetail = new InvoiceDetail
                {
                    InvoiceId = invoiceId,
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Book.Price
                };
                _context.InvoiceDetails.Add(newInvoiceDetail);
            }

            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult BuyHistory(int userId, string sdt, string address, string note, int invoiceId)
        {
            var newInvoice = new Invoice
            {
                UserId = userId,
                Sdt = sdt,
                Address = address,
                Note = note,
                Date = DateTime.Now,
            };

            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();

            var newinvoiceId = _context.Invoices.Max(i => i.InvoiceId);
            var oldInvoice = _context.Invoices
                .Where(i => i.InvoiceId == invoiceId)
                .Include(i => i.InvoiceDetails)
                .ThenInclude(b => b.Book)
                .FirstOrDefault();

            foreach (var invoiceDetail in (List<InvoiceDetail>)oldInvoice.InvoiceDetails)
            {
                var newInvoiceDetail = new InvoiceDetail
                {
                    InvoiceId = newinvoiceId,
                    BookId = invoiceDetail.BookId,
                    Quantity = invoiceDetail.Quantity,
                    UnitPrice = invoiceDetail.Book.Price
                };
                _context.InvoiceDetails.Add(newInvoiceDetail);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult BuyProduct(int bookId, decimal price, int quantity, int userId, string sdt, string address, string note)
        {
            var newInvoice = new Invoice
            {
                UserId = userId,
                Sdt = sdt,
                Address = address,
                Note = note,
                Date = DateTime.Now,
            };

            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();

            var invoiceId = _context.Invoices.Max(i => i.InvoiceId);
            var newInvoiceDetail = new InvoiceDetail
            {
                InvoiceId = invoiceId,
                BookId = bookId,
                UnitPrice = price,
                Quantity = quantity
            };
            _context.InvoiceDetails.Add(newInvoiceDetail);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
