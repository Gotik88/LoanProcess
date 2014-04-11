// ============================================================================
// <copyright file="TextEmailService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Notification
{
    using System;
    using System.Text;

    using LoanProcess.Infrastructure.Logging;

    public class TextEmailService : IEmailService
    {
        public void SendMail(string from, string to, string subject, string body)
        {
            StringBuilder email = new StringBuilder();
            email.AppendLine(string.Format("To: {0}", to));
            email.AppendLine(string.Format("From: {0}", from));
            email.AppendLine(string.Format("Subject: {0}", subject));
            email.AppendLine(string.Format("Body: {0}", body));
            LoggingFactory.GetLogger().Log(email.ToString());
        }
    }
}
