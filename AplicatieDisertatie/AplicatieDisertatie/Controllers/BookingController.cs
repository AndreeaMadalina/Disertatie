using AplicatieDisertatie.Models.DTO;
using Common;
using Common.DTO;
using EmailSender;
using Service.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AplicatieDisertatie.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Booking()
        {
            BookingModel model = new BookingModel();
            ViewBag.GetHours = new SelectList(Constants.AvailableHoursToBook);
            return View();
        }

        [HttpPost]
        public ActionResult Register(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    EmailSenderRepository emailRepository = new EmailSenderRepository();
                    BookingReportModel reportModel = new BookingReportModel();
                    reportModel.FullName = model.FirstName + " " + model.LastName;
                    reportModel.BookingDateToString = model.BookingDate.Value.ToString("dd/MM/yyyy");
                    reportModel.BookingHour = model.HourText;
                    reportModel.Email = model.Email;
                    reportModel.InstitutionName = "Car registration";
                    EmailTemplateType emailTemplateType = emailRepository.GetEmailTemplate();
                    EmailSender.EmailSender.SendEmail(emailTemplateType, reportModel);


                    model.InstitutionID = (int)Constants.enmInstitutions.RegisterCar;
                    BookingRepository bookingRepository = new BookingRepository();
                    bookingRepository.SaveBooking(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetAvailableHoursForSelectedDate(string selectedDate)
        {
            DateTime.TryParse(selectedDate, out DateTime date);
            if (date != null && selectedDate != string.Empty)
            {
                BookingRepository bookingRepository = new BookingRepository();
                var bookedHoursForSelectedDate = bookingRepository.GetBookingHoursForSelectedDate(Convert.ToDateTime(selectedDate));
                bookedHoursForSelectedDate = bookedHoursForSelectedDate.Distinct().ToList();
                var listToDisplay = Constants.AvailableHoursToBook.Except(bookedHoursForSelectedDate).ToList();
                return Json(listToDisplay, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<string>(), JsonRequestBehavior.AllowGet);
        }
    }
}