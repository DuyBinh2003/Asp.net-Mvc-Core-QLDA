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
    }
}
