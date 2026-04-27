using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class VehicleMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
