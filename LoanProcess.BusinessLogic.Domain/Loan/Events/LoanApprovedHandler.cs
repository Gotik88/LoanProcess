// ============================================================================
// <copyright file="LoanApprovedHandler.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.Domain
{
    using LoanProcess.BusinessLogic.DomainEvents;

    public class LoanApprovedHandler : IHandles<LoanApproved>
    {
        public void Handle(LoanApproved args)
        {
            // send email to Notification service
        }
    }
}
