using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StockReturnController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
