// ============================================================================
// <copyright file="GetLoansHandler.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer.Handlers
{
    using System;
    using LoanProcess.ServiceLayer.Messages.Loan;
    using LoanProcess.BusinessLogic.IDomainServices;

    public class GetLoansHandler : IRequestHandler<GetLoansRequest, GetLoansResponse>
    {
        private readonly ILoanService _loanService;

        public GetLoansResponse ProcessRequest(GetLoansRequest request)
        {
            var response = new GetLoansResponse();

            try
            {
                response.Loans = _loanService.GetLoans();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
