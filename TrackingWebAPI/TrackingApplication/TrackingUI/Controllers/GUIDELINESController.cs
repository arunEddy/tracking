using Microsoft.AspNetCore.Mvc;

namespace TrackingUI.Controllers
{
    public class GUIDELINESController : Controller
    {
        public IActionResult PROHIBITEDRESTRICTEDITEMS()
        {
            return View();
        }
        public IActionResult CUSTOMSDOCUMENTATIONGUIDE()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        
    }
}
