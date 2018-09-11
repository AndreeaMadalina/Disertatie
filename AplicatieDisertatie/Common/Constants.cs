using System.Collections.Generic;

namespace Common
{
    public static class Constants
    {
        public static int MapToShow;

        public static string ConnectionString = @"Server=DESKTOP-5U20CQ0\SQLEXPRESS1;Database=BookingApplication;Trusted_Connection=True;";

        public static List<string> AvailableHoursToBook = new List<string>()
        {
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
        };

        public static List<string> ActionsForIdentification = new List<string>()
        {
            "ID",
            "Passport",
        };

        public static List<string> ActionsForReport = new List<string>()
        {
            "Car registration",
            "ID or passport",
            "Driver license",
            "Car elimination",
        };

        public enum enmInstitutions
        {
            RegisterCar = 1,
            IdentificationCardOrPassport = 2,
            DriverLicense = 3,
            CarDeletion = 4
        }
    }
}
