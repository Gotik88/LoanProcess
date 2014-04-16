// ============================================================================
// <copyright file="GetLoansResponse.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer.Messages.Loan
{
    using System.Collections.Generic;
    using LoanProcess.BusinessLogic.Domain;

    public class GetLoansResponse : ResponseBase
    {
        public IList<Loan> Loans { get; set; }
    }
}