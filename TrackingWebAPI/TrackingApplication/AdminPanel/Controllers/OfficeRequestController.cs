using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class OfficeRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
