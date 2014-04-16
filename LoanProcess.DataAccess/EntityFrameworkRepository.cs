// ============================================================================
// <copyright file="IDbContext.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

using LoanProcess.BusinessLogic;

namespace LoanProcess.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using BusinessLogic.Domain;

    public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public EntityFrameworkRepository(IDbContext context)
        {
            this._context = context;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                return _entities ?? _context.Set<T>();
            }
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = string.Empty;

                /* foreach (var validationErrors in dbEx.EntityValidationErrors)
                     foreach (var validationError in validationErrors.ValidationErrors)
                         msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;*/

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = string.Empty;

                /*foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);*/

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = string.Empty;

                /*foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);*/

                var fail = new Exception(msg, ex);
                throw fail;
            }
        }
    }
}
