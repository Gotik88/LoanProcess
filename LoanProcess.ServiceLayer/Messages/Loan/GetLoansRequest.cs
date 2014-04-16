// ============================================================================
// <copyright file="GetLoansRequest.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer.Messages.Loan
{
    public class GetLoansRequest: IRequestData<GetLoansResponse>
    {
        public int LoanId { get; set; }
    }
}
