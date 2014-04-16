// ============================================================================
// <copyright file="ILoanService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.IDomainServices
{
    using System.Collections.Generic;
    using LoanProcess.BusinessLogic.Domain;

    public interface ILoanService
    {
        Loan GetLoanById(int loanId);

        IList<Loan> GetLoans();

        void InsertLoan(Loan loan);

        void UpdateLoan(Loan loan);

        void DeleteLoan(int loanId);
    }
}
