using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
