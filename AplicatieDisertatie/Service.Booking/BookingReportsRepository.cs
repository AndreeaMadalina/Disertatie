using Common;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Booking
{
    public class BookingReportsRepository
    {
        public List<BookingReportModel> GetBookingReports(string tableName, string institutionDisplayName)
        {
            List<BookingReportModel> bookingReports = new List<BookingReportModel>();
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = String.Format("SELECT [FirstName], [LastName], [BookingDate], [BookingHour], [InstitutionName]" +
                    "                                FROM " + tableName +
                    "                                INNER JOIN Institutions ON Institutions.InstitutionID = " + tableName + ".[InstitutionID]" +
                    "                                WHERE Institutions.[InstitutionDisplayName] = @InstitutionDisplayName");

                command.Parameters.Add("@InstitutionDisplayName", SqlDbType.NVarChar).Value = institutionDisplayName;


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookingReportModel booking = new BookingReportModel();
                        if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                        {
                            booking.FullName = reader.GetString(reader.GetOrdinal("FirstName"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("LastName")))
                        {
                            booking.FullName += ' ' + reader.GetString(reader.GetOrdinal("LastName"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("BookingDate")))
                        {
                            booking.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
                            booking.BookingDateToString = booking.BookingDate.ToString("dd/MM/yyyy");
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("BookingHour")))
                        {
                            booking.BookingHour = reader.GetString(reader.GetOrdinal("BookingHour"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("InstitutionName")))
                        {
                            booking.InstitutionName = reader.GetString(reader.GetOrdinal("InstitutionName"));
                        }
                        bookingReports.Add(booking);
                    }
                }
                connection.Close();
            }

            return bookingReports;
        }
    }
}
