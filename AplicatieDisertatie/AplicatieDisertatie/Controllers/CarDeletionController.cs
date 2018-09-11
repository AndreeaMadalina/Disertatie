using AplicatieDisertatie.Models.DTO;
using Common;
using Common.DTO;
using Service.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieDisertatie.Controllers
{
    public class CarDeletionController : Controller
    {
        // GET: CarDeletion
        public ActionResult CarDeletion()
        {
            CarDeletionModel model = new CarDeletionModel();
            ViewBag.GetHours = new SelectList(Constants.AvailableHoursToBook);
            return View();
        }

        [HttpPost]
        public ActionResult Register(CarDeletionModel model)
        {
            if (model != null)
            {
                EmailSenderRepository emailRepository = new EmailSenderRepository();
                BookingReportModel reportModel = new BookingReportModel();
                reportModel.FullName = model.FirstName + " " + model.LastName;
                reportModel.BookingDateToString = model.BookingDate.ToString("dd/MM/yyyy");
                reportModel.BookingHour = model.HourText;
                reportModel.Email = model.Email;
                reportModel.InstitutionName = "Car deletion";
                EmailTemplateType emailTemplateType = emailRepository.GetEmailTemplate();
                EmailSender.EmailSender.SendEmail(emailTemplateType, reportModel);

                model.InstitutionID = (int)Constants.enmInstitutions.CarDeletion;
                CarDeletionRepository bookingRepository = new CarDeletionRepository();
                bookingRepository.SaveBooking(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetAvailableHoursForSelectedDate(string selectedDate)
        {
            CarDeletionRepository bookingRepository = new CarDeletionRepository();
            var bookedHoursForSelectedDate = bookingRepository.GetBookingHoursForSelectedDate(Convert.ToDateTime(selectedDate));
            bookedHoursForSelectedDate = bookedHoursForSelectedDate.Distinct().ToList();
            var listToDisplay = Constants.AvailableHoursToBook.Except(bookedHoursForSelectedDate).ToList();
            return Json(listToDisplay, JsonRequestBehavior.AllowGet);
        }
    }
}