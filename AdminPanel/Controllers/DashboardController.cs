using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
