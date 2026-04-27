using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminPanel.Controllers
{
    public class InScanController : Controller
    {
        public IActionResult CashBooking()
        {
            return View();
        }

        public IActionResult BookingScanNormal() {
            return View();
        }
        public IActionResult INSCANLOGISTIC()
        { return View(); }

        public IActionResult ReBookingScan()
        { return View(); }

        public IActionResult ReturnBooking()
        { return View(); }

        public IActionResult EDPEntry()
        { return View(); }

        public IActionResult BookingScanNormalEdit()
        { return View(); }

        public IActionResult BookingScanLogisticEdit()
        { return View(); }

        public IActionResult EDPEditing()
        { return View(); }

        public IActionResult WMSStockUploadWithCustomerWise()
        { return View(); }

        public IActionResult OperationDataImport()
        { return View(); }

        public IActionResult UrgentDeliveryAlertEntry()
        { return View(); }

        public IActionResult UrgentDeliveryAlertUpdation()
        { return View(); }

        public IActionResult AWBPrint()
        { return View(); }

     

    }
}
