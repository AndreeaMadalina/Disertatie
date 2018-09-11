﻿using Common;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Service.Booking
{
    public class CarDeletionRepository
    {
        public bool SaveBooking(CarDeletionModel request)
        {
            if (request != null)
            {
                using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO [CarDeletionBooking] ([InstitutionID], [FirstName], [LastName], [Email], [CNP], [BookingDate], [BookingHour], [PlateNumber]) 
                                        VALUES (@InstitutionID, @FirstName, @LastName, @Email, @CNP, @BookingDate, @BookingHour, @PlateNumber)";

                    command.Parameters.Add("@InstitutionID", SqlDbType.Int).Value = request.InstitutionID;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = request.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = request.LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = request.Email;
                    command.Parameters.Add("@CNP", SqlDbType.NVarChar).Value = request.CNP;
                    command.Parameters.Add("@BookingDate", SqlDbType.DateTime).Value = request.BookingDate;
                    command.Parameters.Add("@BookingHour", SqlDbType.NVarChar).Value = request.HourText;
                    command.Parameters.Add("@PlateNumber", SqlDbType.NVarChar).Value = request.PlateNumber;

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
                command.CommandText = @"SELECT BookingHour FROM [CarDeletionBooking] WHERE BookingDate = @BookingDay";

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
