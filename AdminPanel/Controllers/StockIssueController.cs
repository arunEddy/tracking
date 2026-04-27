using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class StockIssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
