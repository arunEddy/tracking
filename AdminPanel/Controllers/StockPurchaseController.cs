using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StockPurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
