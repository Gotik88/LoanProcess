// ============================================================================
// <copyright file="IRequestResponseFactory.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.ServiceLayer
{
    public interface IRequestResponseFactory
    {
        TResponse ProcessRequest<TRequest, TResponse>(TRequest request) where TRequest : IRequestData<TResponse>;
    }
}
