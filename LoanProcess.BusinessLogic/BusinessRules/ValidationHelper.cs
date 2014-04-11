// ============================================================================
// <copyright file="ValidationHelper.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.BusinessRules
{
    using System;
    using System.Linq;
    using System.Text;

    public static class ValidationHelper
    {
        public static void ThrowExceptionIfInvalid(BaseEntity entity)
        {
            /*if (entity.ValidationErrors)
            {
                var brokenRules = new StringBuilder();
                brokenRules.AppendLine(string.Format("There were problems saving the {0}:", entity));

                foreach (BusinessRule businessRule in entity.GetBrokenRules())
                {
                    brokenRules.AppendLine(businessRule.Rule);
                }

                throw new ApplicationException(brokenRules.ToString());
            }*/
        }
    }
}
