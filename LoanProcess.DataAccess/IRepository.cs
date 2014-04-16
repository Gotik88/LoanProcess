// ============================================================================
// <copyright file="IRepository.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

using LoanProcess.BusinessLogic;

namespace LoanProcess.DataAccess
{
    using System;
    using System.Linq;

    using LoanProcess.BusinessLogic.Domain;

    public interface IRepository<T> where T : BaseEntity
    {
        //IQueryable<T> FindAll();

        //T FindBy(Guid id);

        //void Add(T entity);

        //void Save(T entity);

        //void Remove(T entity);

        void Insert(T entity);
    }
}
