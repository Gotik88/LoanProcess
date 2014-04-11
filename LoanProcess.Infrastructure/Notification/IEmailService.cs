// ============================================================================
// <copyright file="IEmailService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Notification
{
    public interface IEmailService
    {
        void SendMail(string from, string to, string subject, string body);
    }
}
