// ============================================================================
// <copyright file="IHandles.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.DomainEvents
{
    public interface IHandles<in T> where T : IDomainEvent
    {
        void Handle(T args);
    }
}
