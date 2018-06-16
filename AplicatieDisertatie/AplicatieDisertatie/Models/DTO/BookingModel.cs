using System;
using System.ComponentModel.DataAnnotations;

namespace AplicatieDisertatie.Models.DTO
{
    public class BookingModel
    {
        public int BookingID { get; set; }

        public int InstitutionID { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        public string LastName { get; set; }

        public string CarRegistrationNumber { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public int BookingTypeID {get; set;}
    }
}