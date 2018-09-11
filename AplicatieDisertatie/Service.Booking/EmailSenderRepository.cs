using Common;
using Common.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Service.Booking
{
    public class EmailSenderRepository
    {
        public EmailTemplateType GetEmailTemplate()
        {
            List<EmailTemplateType> emailTemplates = new List<EmailTemplateType>();

            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM [EmailTemplate] WHERE [EmailTemplateType] = 'Normal'";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmailTemplateType emailTemplate = new EmailTemplateType();
                        if (!reader.IsDBNull(reader.GetOrdinal("EmailTemplateSubject")))
                        {
                            emailTemplate.EmailTemplateSubject = reader.GetString(reader.GetOrdinal("EmailTemplateSubject"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("EmailTemplateBody")))
                        {
                            emailTemplate.EmailTemplateBody = reader.GetString(reader.GetOrdinal("EmailTemplateBody"));
                        }
                        emailTemplates.Add(emailTemplate);
                        break;
                    }
                }
                connection.Close();
            }
            return emailTemplates[0];
        }
    }
}
