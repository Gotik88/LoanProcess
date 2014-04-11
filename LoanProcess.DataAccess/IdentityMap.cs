// ============================================================================
// <copyright file="IdentityMap.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.DataAccess
{
    using System;
    using System.Collections;

    public class IdentityMap<T>
    {
        private readonly Hashtable _entities = new Hashtable();

        public T GetById(Guid id)
        {
            if (_entities.ContainsKey(id))
            {
                return (T)_entities[id];
            }

            return default(T);
        }

        public void Store(T entity, Guid key)
        {
            if (!_entities.Contains(key))
            {
                _entities.Add(key, entity);
            }
        }
    }
}
