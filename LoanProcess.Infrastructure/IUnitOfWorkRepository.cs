// ============================================================================
// <copyright file="IUnitOfWorkRepository.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IAggregateRoot entity);

        void PersistUpdateOf(IAggregateRoot entity);

        void PersistDeletionOf(IAggregateRoot entity);
    }
}
