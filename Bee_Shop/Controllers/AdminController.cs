using Microsoft.AspNetCore.Mvc;

namespace Bee_Shop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
