using Microsoft.AspNetCore.Mvc;

namespace Navio_Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
