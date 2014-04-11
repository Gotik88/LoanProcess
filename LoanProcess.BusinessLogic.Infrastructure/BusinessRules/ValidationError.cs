// ============================================================================
// <copyright file="ValidationError.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.BusinessRules
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }

        public string PropertyName { get; set; }

        public override string ToString()
        {
            return this.ErrorMessage;
        }
    }
}
