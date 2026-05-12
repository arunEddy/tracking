using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class DRSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
