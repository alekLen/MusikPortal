using Microsoft.AspNetCore.Mvc;

namespace MusikPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
