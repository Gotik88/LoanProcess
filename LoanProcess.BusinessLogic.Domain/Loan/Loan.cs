// ============================================================================
// <copyright file="Loan.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.Domain
{
    using System.Collections.Generic;

    using LoanProcess.BusinessLogic.BusinessRules;

    public class Loan : BaseEntity
    {
        public LoanStatus LoanStatus { get; set; }

        protected override IEnumerable<ValidationError> Validate()
        {
            if (LoanStatus.Equals(LoanStatus.Borrowed))
            {
                AddBrokenRule(LoanBusinessRules.LoanRequired);
            }

            return base.Validate();
        }
    }
}
