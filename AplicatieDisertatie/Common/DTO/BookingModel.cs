using System;
using System.ComponentModel.DataAnnotations;

namespace AplicatieDisertatie.Models.DTO
{
    public class BookingModel
    {
        public int BookingID { get; set; }

        public int InstitutionID { get; set; }

        [Display(Name = "First Name: *")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name: *")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Email: *")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "CNP: *")]
        [Required(ErrorMessage = "CNP is required")]
        public string CNP { get; set; } = string.Empty;

        [Display(Name = "Date: *")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime? BookingDate { get; set; } = DateTime.Now;

        [Display(Name = "Select an hour: *")]
        [Required(ErrorMessage = "Time is required")]
        public string Hour { get; set; } = string.Empty;

        public string HourText { get; set; }

    }
}