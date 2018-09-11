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
    public class IdentificationCardOrPassportController : Controller
    {
        // GET: IdentificationCardOrPassport
        public ActionResult IdentificationCardOrPassport()
        {
            IdentificationCardOrPassportModel model = new IdentificationCardOrPassportModel();
            ViewBag.GetHours = new SelectList(Constants.AvailableHoursToBook);
            ViewBag.ActiosForIdentification = new SelectList(Constants.ActionsForIdentification);
            return View();
        }

        [HttpPost]
        public ActionResult Register(IdentificationCardOrPassportModel model)
        {
            if (model != null)
            {
                EmailSenderRepository emailRepository = new EmailSenderRepository();
                BookingReportModel reportModel = new BookingReportModel();
                reportModel.FullName = model.FirstName + " " + model.LastName;
                reportModel.BookingDateToString = model.BookingDate.ToString("dd/MM/yyyy");
                reportModel.BookingHour = model.HourText;
                reportModel.Email = model.Email;
                reportModel.InstitutionName = model.ActionNameText;
                EmailTemplateType emailTemplateType = emailRepository.GetEmailTemplate();
                EmailSender.EmailSender.SendEmail(emailTemplateType, reportModel);

                model.InstitutionID = (int)Constants.enmInstitutions.IdentificationCardOrPassport;
                IdentificationRepository bookingRepository = new IdentificationRepository();
                bookingRepository.SaveBooking(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult GetAvailableHoursForSelectedDate(string selectedDate)
        {
            IdentificationRepository bookingRepository = new IdentificationRepository();
            var bookedHoursForSelectedDate = bookingRepository.GetBookingHoursForSelectedDate(Convert.ToDateTime(selectedDate));
            bookedHoursForSelectedDate = bookedHoursForSelectedDate.Distinct().ToList();
            var listToDisplay = Constants.AvailableHoursToBook.Except(bookedHoursForSelectedDate).ToList();
            return Json(listToDisplay, JsonRequestBehavior.AllowGet);
        }
    }
}