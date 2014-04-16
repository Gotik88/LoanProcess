// ============================================================================
// <copyright file="IRequestHandler.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer
{
    public interface IRequestHandler<in TRequest, out TResponse> where TRequest : IRequestData<TResponse>
    {
        TResponse ProcessRequest(TRequest request);
    }
}
