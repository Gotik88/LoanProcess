// ============================================================================
// <copyright file="ILoanService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.IDomainServices
{
    using LoanProcess.BusinessLogic.Domain;

    public interface ILoanService
    {
        void InsertLoan(Loan loan);

        void UpdateLoan(Loan loan);

        void DeleteLoan(int loanId);
    }
}
