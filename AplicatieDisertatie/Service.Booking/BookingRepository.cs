﻿using AplicatieDisertatie.Models.DTO;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Booking
{
    public class BookingRepository
    {
        public BookingRepository()
        {

        }

        public bool SaveBooking(BookingModel request)
        {
            if (request != null)
            {
                using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO [Bookings] ([InstitutionID], [FirstName], [LastName], [Email], [CNP], [BookingDate], [BookingHour]) 
                                        VALUES (@InstitutionID, @FirstName, @LastName, @Email, @CNP, @BookingDate, @BookingHour)";

                    command.Parameters.Add("@InstitutionID", SqlDbType.Int).Value = request.InstitutionID;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = request.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = request.LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = request.Email;
                    command.Parameters.Add("@CNP", SqlDbType.NVarChar).Value = request.CNP;
                    command.Parameters.Add("@BookingDate", SqlDbType.DateTime).Value = request.BookingDate;
                    command.Parameters.Add("@BookingHour", SqlDbType.NVarChar).Value = request.HourText;

                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }

            return false;
        }

        public List<string> GetBookingHoursForSelectedDate(DateTime selectedDate)
        {
            List<string> bookingsForSelectedDate = new List<string>();
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT BookingHour FROM [Bookings] WHERE BookingDate = @BookingDay";

                command.Parameters.Add("@BookingDay", SqlDbType.DateTime).Value = selectedDate;


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("BookingHour")))
                        {
                            bookingsForSelectedDate.Add(reader.GetString(reader.GetOrdinal("BookingHour")));
                        }
                    }
                }
                connection.Close();
            }

            return bookingsForSelectedDate;
        }
    }
}
