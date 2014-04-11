// ============================================================================
// <copyright file="GetLoanResponse.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer.Messages.Loan
{
    using Models;

    public class GetLoanResponse : ResponseBase
    {
        public LoanView Loan { get; set; }
    }
}