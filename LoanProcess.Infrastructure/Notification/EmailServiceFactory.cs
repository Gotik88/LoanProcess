// ============================================================================
// <copyright file="EmailServiceFactory.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Notification
{
    public static class EmailServiceFactory
    {
        private static IEmailService _emailService;

        public static void InitializeEmailServiceFactory(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public static IEmailService GetEmailService()
        {
            return _emailService;
        }
    }
}
