using Common.DTO;
using System;
using System.Net.Mail;

namespace EmailSender
{
    public static class EmailSender
    {
        public static bool SendEmail(EmailTemplateType emailTemplate, BookingReportModel reportModel)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("booking.gov.noreply@gmail.com");
                mail.To.Add(reportModel.Email);
                mail.Subject = emailTemplate.EmailTemplateSubject.Trim();
                mail.Body = String.Format(emailTemplate.EmailTemplateBody, reportModel.FullName, reportModel.InstitutionName, reportModel.BookingDateToString, reportModel.BookingHour);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("booking.gov.noreply@gmail.com", "Disertatie123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
