// ============================================================================
// <copyright file="EntityRepositoryBase.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Threading;

namespace LoanProcess.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using BusinessLogic.Domain;
    using LoanProcess.BusinessLogic;
    using System.Data.Entity.Core;

    public class EntityRepositoryBase<T> : IRepository<T> where T : BaseEntity, IDisposable
    {
        /*private readonly IDbContext _context;
        private IDbSet<T> _entities;*/

        private ObjectContext _context;

        private EntityRepositoryBase(ObjectContext objectContext)
        {
            InstanceLockObject = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            Contract.Requires<ArgumentNullException>(objectContext != null);

            _context = objectContext;
        }

        public event EventHandler<EventArgs> BeforeSaveChanges;

        public event EventHandler<EventArgs> AfterSaveChanges;

        public ReaderWriterLockSlim InstanceLockObject { get; private set; }

        protected internal ObjectContext Context
        {
            get
            {
                if (_context == null)
                {
                    Contract.Assert(_context != null);
                }

                return _context;
            }
        }

        public int SaveAll<T>(IEnumerable<T> entities)
        {
            var result = 0;
            foreach (var entity in entities)
            {
                Save(entity);
                result++;
            }

            return result;
        }

        /// <summary>
        /// Saves the entity of specific type.
        /// </summary>
        public void Save<TEntity>(TEntity entity)
        {
           /* if (CanAdd(entity))
            {
                Add(entity);
            }
            else if (CanUpdate(entity))
            {
                Update(entity);
            }
            else
            {
                throw new ArgumentException("entity");
            }*/
        }

        /*public virtual IQueryable<T> Table
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
        }*/

        #region Load

        public TEntity LoadByKey<TEntity, TKey>(TKey key)
        {
            var entityKey = (key as EntityKey) ?? CreateEntityKey<TEntity, TKey>(key);

            InstanceLockObject.EnterWriteLock();
            try
            {
                return (TEntity)Context.GetObjectByKey(entityKey);
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public IEnumerable<TEntity> LoadAll<TEntity>(Func<TEntity, bool> whereCondition)
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                return whereCondition != null
                           ? GetQuery<TEntity>().Where(whereCondition)
                           : GetQuery<TEntity>().AsEnumerable();
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public IQueryable<TEntity> GetQuery<TEntity>()
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                var type = typeof(T);
                var entityType = GetBaseType(type);
                var set = GetEntitySet<TEntity>();
                var entitySetName = string.Format("{0}.{1}", set.EntityContainer.Name, set.Name);
                var objectQuery = Context.CreateQuery<TEntity>(entitySetName);

                return entityType == type ? objectQuery : objectQuery.OfType<TEntity>();
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public IEnumerable<TEntity> LoadAllWhere<TEntity>(Expression<Func<TEntity, bool>> whereCondition)
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                return whereCondition != null
                           ? GetQuery<TEntity>().Where(whereCondition).AsEnumerable()
                           : GetQuery<TEntity>().AsEnumerable();
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public TEntity Load<TEntity>(Expression<Func<TEntity, bool>> whereCondition)
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                return whereCondition != null
                           ? GetQuery<TEntity>().Where(whereCondition).FirstOrDefault()
                           : GetQuery<TEntity>().FirstOrDefault();
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        #endregion

        /*public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }*/

        #region AddUpdateDelete

        public void Insert(T entity)
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                Context.AddObject(GetEntitySetName<T>(), entity);
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public void Add<TEntity>(TEntity entity)
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                Context.AddObject(GetEntitySetName<T>(), entity);
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        public void Update<TEntity>(TEntity entity)
        {
            var entityObject = entity as EntityObject;
            if (entityObject == null || entityObject.EntityState == EntityState.Deleted)
            {
                return;
            }

            InstanceLockObject.EnterUpgradeableReadLock();
            try
            {
                var changedObject = GetChangedObject(entity);
                var entitySetName = GetEntitySetName<T>();
                InstanceLockObject.EnterWriteLock();

                try
                {
                    if (changedObject == null)
                    {
                        Context.AddObject(entitySetName, entity);
                    }

                    if (entityObject.EntityState == EntityState.Added ||
                        entityObject.EntityState == EntityState.Unchanged)
                    {
                        Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    }

                    //// Apply current values every time - it will cause updating of entity relations.
                    if (entityObject != changedObject)
                    {
                        Context.ApplyCurrentValues(entitySetName, entity as object);
                    }
                }
                finally
                {
                    InstanceLockObject.ExitWriteLock();
                }
            }
            finally
            {
                InstanceLockObject.ExitUpgradeableReadLock();
            }
        }

        public void Delete<TEntity>(TEntity entity)
        {
            var entityObject = entity as EntityObject;
            if (entityObject == null)
            {
                return;
            }

            InstanceLockObject.EnterUpgradeableReadLock();
            try
            {
                var changedObject = GetChangedObject(entity);
                var entitySetName = GetEntitySetName<T>();
                InstanceLockObject.EnterWriteLock();
                try
                {
                    if (changedObject == null)
                    {
                        Context.AddObject(entitySetName, entity);
                    }

                    if (entityObject.EntityState == EntityState.Added ||
                        entityObject.EntityState == EntityState.Unchanged)
                    {
                        Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                    }

                    Context.DeleteObject(changedObject ?? entity);
                }
                finally
                {
                    InstanceLockObject.ExitWriteLock();
                }
            }
            finally
            {
                InstanceLockObject.ExitUpgradeableReadLock();
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        /*public virtual void Insert(T entity)
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
                var msg = string.Empty;*/

        /* foreach (var validationErrors in dbEx.EntityValidationErrors)
             foreach (var validationError in validationErrors.ValidationErrors)
                 msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;*/

        /* var fail = new Exception(msg, ex);
         throw fail;
     }
 }*/

        /*public virtual void Update(T entity)
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
                var msg = string.Empty;*/

                /*foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);*/

                /*var fail = new Exception(msg, ex);
                throw fail;
            }
        }*/

        /*public virtual void Delete(T entity)
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
                var msg = string.Empty;*/

                /*foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);*/

                /*var fail = new Exception(msg, ex);
                throw fail;
            }
        }*/

        #region Entities

        protected virtual EntityKey CreateEntityKey<TEntity, TKey>(TKey key)
        {
            var set = GetEntitySet<TEntity>();

            return new EntityKey(
                string.Format("{0}.{1}", set.EntityContainer.Name, set.Name),
                set.ElementType.KeyMembers.Single().Name,
                key);
        }

        /// <summary>
        /// Returns the entity set name from the entity type.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <returns>The name of entity set.</returns>
        protected string GetEntitySetName<T>()
        {
            var set = GetEntitySet<T>();

            return string.Format("{0}.{1}", set.EntityContainer.Name, set.Name);
        }

        /// <summary>
        /// Returns the entity set for the entity type.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <returns>The entity set.</returns>
        protected virtual EntitySetBase GetEntitySet<T>()
        {
            var type = typeof(T);
            var entityType = GetBaseType(type);
            var entitySet = GetEntitySet(entityType);

            if (entitySet == null)
            {
                throw new ArgumentException();
            }

            return entitySet;
        }

        /// <summary>
        /// Returns actual entity instance from Object Context by specified entity key
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entity">Entity instance</param>
        /// <returns>Entity from Object Context</returns>
        private object GetChangedObject<T>(T entity)
        {
            var key = GetEntityKey(entity);
            if (key == null)
            {
                return null;
            }

            var entry = GetObjectEntry(key);

            return entry == null ? null : entry.Entity;
        }

        private EntityKey GetEntityKey<T>(T entity)
        {
            var entityWithKey = entity as IEntityWithKey;
            if (entityWithKey == null)
            {
                return null;
            }

            return entityWithKey.EntityKey ?? Context.CreateEntityKey(GetEntitySetName<T>(), entity);
        }

        /// <summary>
        /// Returns entity entry from Object Context by specified entity key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>The entity entry from Object Context.</returns>
        private ObjectStateEntry GetObjectEntry(EntityKey key)
        {
            ObjectStateEntry entry;

            return Context.ObjectStateManager.TryGetObjectStateEntry(key, out entry) ? entry : null;
        }

        /// <summary>
        /// Returns the entity set name from the entity type.
        /// </summary>
        /// <param name="entityType">The entity type.</param>
        /// <returns>The name of entity set.</returns>
        private string GetEntitySetName(Type entityType)
        {
            var set = GetEntitySet(GetBaseType(entityType));

            return string.Format("{0}.{1}", set.EntityContainer.Name, set.Name);
        }

        /// <summary>
        /// Returns the entity set for the entity type.
        /// </summary>
        /// <param name="entityType">The entity type.</param>
        /// <returns>The entity set.</returns>
        private EntitySetBase GetEntitySet(Type entityType)
        {
            var attrs = entityType.GetCustomAttributes(typeof(EdmEntityTypeAttribute), true);
            if (attrs == null || attrs.Length == 0)
            {
                return null;
            }

            var attr = (EdmEntityTypeAttribute)attrs[0];
            var containers = Context
                .MetadataWorkspace
                .GetItems<EntityContainer>(DataSpace.CSpace);

            return
                containers
                    .SelectMany(container => container.BaseEntitySets)
                    .Where(set =>
                        attr.NamespaceName == set.ElementType.NamespaceName &&
                        attr.Name == set.ElementType.Name)
                    .FirstOrDefault(set => set != null) ??
                containers
                    .Select(container => GetEntitySet(container, entityType))
                    .FirstOrDefault(set => set != null);
        }

        /// <summary>
        /// Returns the entity set for the entity type.
        /// </summary>
        /// <param name="container">The entity container.</param>
        /// <param name="entityType">The type of entity.</param>
        /// <returns>The entity set.</returns>
        private EntitySetBase GetEntitySet(EntityContainer container, Type entityType)
        {
            /*if (_contextAssembly == null)
            {
                _contextAssembly = (from set in container.BaseEntitySets
                                    from type in GetAppDomainTypes()
                                    let attrs = type.GetCustomAttributes(typeof(EdmEntityTypeAttribute), true)
                                    where attrs != null && attrs.Length > 0
                                    let attr = (EdmEntityTypeAttribute)attrs[0]
                                    where attr.NamespaceName == set.ElementType.NamespaceName &&
                                            attr.Name == set.ElementType.Name
                                    select type.Assembly).FirstOrDefault();
            }

            if (_contextAssembly != null)
            {
                var types = from set in container.BaseEntitySets
                            from type in GetAssemblyTypes()
                            let attrs = type.GetCustomAttributes(typeof(EdmEntityTypeAttribute), true)
                            where attrs != null && attrs.Length > 0
                            let attr = (EdmEntityTypeAttribute)attrs[0]
                            where attr.NamespaceName == set.ElementType.NamespaceName &&
                                    attr.Name == set.ElementType.Name &&
                                    entityType.IsAssignableFrom(type)
                            select set;

                return types.FirstOrDefault();
            }*/

            return null;
        }

        private Type GetBaseType(Type type)
        {
            var baseType = type.BaseType;
            if (baseType != null && baseType != typeof(EntityObject) && baseType != typeof(object))
            {
                return GetBaseType(baseType);
            }

            return type;
        }

        #endregion

        #region SaveChanges

        protected void OnBeforeSaveChanges()
        {
            var handler = BeforeSaveChanges;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises an AfterSaveChanges event.
        /// </summary>
        protected void OnAfterSaveChanges()
        {
            var handler = AfterSaveChanges;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private int SaveChanges()
        {
            InstanceLockObject.EnterWriteLock();
            try
            {
                OnBeforeSaveChanges();

                var result = _context.SaveChanges();

                OnAfterSaveChanges();

                return result;
            }
            finally
            {
                InstanceLockObject.ExitWriteLock();
            }
        }

        #endregion
    }
}
