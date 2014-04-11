// ============================================================================
// <copyright file="LoanBusinessRules.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.BusinessRules
{
    public class LoanBusinessRules
    {
        public static readonly BusinessRule LoanRequired = new BusinessRule("Loan", "A loan is required.");
    }
}
