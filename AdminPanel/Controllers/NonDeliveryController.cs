using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class NonDeliveryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReturnToCustomerManifesting()
        {
            return View();
        }

        public IActionResult FirstMileRTO()
        {
            return View();
        }
    }
}
