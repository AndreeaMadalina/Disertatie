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
    public class DriverLicenseController : Controller
    {
        // GET: DriverLicense
        public ActionResult DriverLicense()
        {
            DriverLicenseModel model = new DriverLicenseModel();
            ViewBag.GetHours = new SelectList(Constants.AvailableHoursToBook);
            return View();
        }

        [HttpPost]
        public ActionResult Register(DriverLicenseModel model)
        {
            if (model != null)
            {
                EmailSenderRepository emailRepository = new EmailSenderRepository();
                BookingReportModel reportModel = new BookingReportModel();
                reportModel.FullName = model.FirstName + " " + model.LastName;
                reportModel.BookingDateToString = model.BookingDate.ToString("dd/MM/yyyy");
                reportModel.BookingHour = model.HourText;
                reportModel.Email = model.Email;
                reportModel.InstitutionName = "Driver license";
                EmailTemplateType emailTemplateType = emailRepository.GetEmailTemplate();
                EmailSender.EmailSender.SendEmail(emailTemplateType, reportModel);

                model.InstitutionID = (int)Constants.enmInstitutions.DriverLicense;
                DriverLicenseRepository bookingRepository = new DriverLicenseRepository();
                bookingRepository.SaveBooking(model);
            }
            return RedirectToAction("Index", "Home");

        }

        public JsonResult GetAvailableHoursForSelectedDate(string selectedDate)
        {
            DriverLicenseRepository bookingRepository = new DriverLicenseRepository();
            var bookedHoursForSelectedDate = bookingRepository.GetBookingHoursForSelectedDate(Convert.ToDateTime(selectedDate));
            bookedHoursForSelectedDate = bookedHoursForSelectedDate.Distinct().ToList();
            var listToDisplay = Constants.AvailableHoursToBook.Except(bookedHoursForSelectedDate).ToList();
            return Json(listToDisplay, JsonRequestBehavior.AllowGet);
        }
    }
}