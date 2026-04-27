using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AirlineMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
