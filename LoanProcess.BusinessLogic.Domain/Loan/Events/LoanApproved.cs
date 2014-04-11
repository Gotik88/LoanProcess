// ============================================================================
// <copyright file="LoanApproved.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.Domain
{
    using LoanProcess.BusinessLogic.DomainEvents;

    public class LoanApproved : IDomainEvent
    {
        public Loan Loan { get; set; }
    }
}
