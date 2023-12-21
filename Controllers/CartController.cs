using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        private readonly CContext _context;

        public CartController(CContext context)
        {  
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = TempData["UserId"];
            if (userId == null || _context.Carts == null)
            {
                return NotFound();
            }
            var carts = await _context.Carts
                .Where(p => p.UserId == int.Parse(userId.ToString()))
                .Include(b => b.Book)
                .ToListAsync();
            if (carts == null)
            {
                return NotFound();
            }
            return View(carts);
        }

    }
}
