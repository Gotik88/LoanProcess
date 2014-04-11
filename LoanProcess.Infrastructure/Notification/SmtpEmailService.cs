// ============================================================================
// <copyright file="SmtpEmailService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Notification
{
    using System.Net.Mail;

    public class SmtpEmailService : IEmailService
    {
        public void SendMail(string from, string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.Subject = subject;
            message.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
        }
    }
}
