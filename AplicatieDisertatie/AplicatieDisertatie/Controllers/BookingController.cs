using AplicatieDisertatie.Models.DTO;
using System.Web.Mvc;

namespace AplicatieDisertatie.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public void Register(BookingModel model)
        {
         if(model != null)
            {
               var firstName = Request.Form["FirstName"];
            }

        }

    }
}