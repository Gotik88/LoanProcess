// ============================================================================
// <copyright file="ResponseBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer.Messages
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
