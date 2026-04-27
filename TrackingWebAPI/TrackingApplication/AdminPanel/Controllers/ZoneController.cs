using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class ZoneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
