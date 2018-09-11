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
    public class BookingReportsController : Controller
    {
        // GET: BookingReports
        public ActionResult BookingReports(string state, BookingReportDateModel formModel)
        {
            ViewBag.States = Constants.ActionsForReport;
            BookingReportsRepository bookingRepository = new BookingReportsRepository();
            BookingReportDateModel bookingReportModel = new BookingReportDateModel();

            if (state != null)
            {
                string tableName = GetTableName(state);
                bookingReportModel.BookingReportModel = bookingRepository.GetBookingReports(tableName, state);
            }
            if (state == null)
            {
                string tableName = "Bookings";
                bookingReportModel.BookingReportModel = bookingRepository.GetBookingReports(tableName, "Car registration");
            }

            IEnumerable<BookingReportModel> model = bookingReportModel.BookingReportModel;
            return View(bookingReportModel);
        }

        public string GetTableName(string state)
        {
            string tableName = string.Empty;
            if (state == "Car registration")
            {
                return "Bookings";
            }

            if (state == "ID or passport")
            {
                return "IdentificationBooking";
            }

            if (state == "Driver license")
            {
                return "DriverLicenseBooking";
            }

            if (state == "Car elimination")
            {
                return "CarDeletionBooking";
            }

            return tableName;
        }
    }
}