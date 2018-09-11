using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class BookingReportDateModel
    {
        public List<BookingReportModel> BookingReportModel = new List<BookingReportModel>();

        [Display(Name = "Start Date")]
        public string StartDateTimeForReport { get; set; }

        [Display(Name = "End Date")]
        public string EndDateTimeForReport { get; set; }

        [Display(Name = "Institution")]
        public string InstitutionName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public DateTime BookingDate { get; set; }

        [Display(Name = "Booking date")]
        public string BookingDateToString { get; set; }

        [Display(Name = "Booking time")]
        public string BookingHour { get; set; }

        public string Email { get; set; }
    }
}
