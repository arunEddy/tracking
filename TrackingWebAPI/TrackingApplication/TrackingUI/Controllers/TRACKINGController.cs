using Microsoft.AspNetCore.Mvc;

namespace TrackingUI.Controllers
{
    public class TRACKINGController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
