using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class DriverLicenseModel
    {
        public int BookingID { get; set; }

        public int InstitutionID { get; set; }

        [Display(Name = "First Name: *")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: *")]
        public string LastName { get; set; }

        [Display(Name = "E-mail: *")]
        public string Email { get; set; }

        [Display(Name = "CNP: *")]
        public string CNP { get; set; }

        [Display(Name = "Select date: *")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Display(Name = "Select an hour: *")]
        public string Hour { get; set; }

        public string HourText { get; set; }
    }
}
