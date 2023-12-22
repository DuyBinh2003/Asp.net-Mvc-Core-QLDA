using Microsoft.AspNetCore.Mvc;

namespace DoAn.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
