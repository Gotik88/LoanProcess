// ============================================================================
// <copyright file="LoanService.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

using System.Collections.Generic;

namespace LoanProcess.BusinessLogic.DomainServices
{
    using LoanProcess.BusinessLogic.BusinessRules;
    using LoanProcess.BusinessLogic.Domain;
    using LoanProcess.BusinessLogic.IDomainServices;

    using LoanProcess.DataAccess;
    using LoanProcess.Infrastructure.UnitOfWork;

    public class LoanService : ILoanService
    {
        private readonly IRepository<Loan> _loanRepository;

        public LoanService(IRepository<Loan> loanRepository)
        {
            this._loanRepository = loanRepository;
        }

        public IList<Loan> GetLoans()
        {
            return new List<Loan>();
        }

        public Loan GetLoanById(int loanId)
        {
            return new Loan();
        }

        public void InsertLoan(Loan loan)
        {
            using (var tx = TransactQueryScope.Create(true))
            {
                _loanRepository.Add(loan);
                ValidationHelper.ThrowExceptionIfInvalid(loan);

                tx.ExecutionContext.CompleteExecute();
            }
        }

        public void UpdateLoan(Loan loan)
        {
        }

        public void DeleteLoan(int loanId)
        {
        }
    }
}
