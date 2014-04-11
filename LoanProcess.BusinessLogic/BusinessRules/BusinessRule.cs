// ============================================================================
// <copyright file="BusinessRule.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.BusinessRules
{
    using System;

    public class BusinessRule
    {
        public BusinessRule(string property, string rule)
        {
            this.Id = Guid.NewGuid();
            this.Property = property;
            this.Rule = rule;
        }

        public Guid Id { get; private set; }

        public string Property { get; private set; }

        public string Rule { get; private set; }
    }
}
