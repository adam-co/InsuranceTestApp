using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace InsuranceTestApp.Repositories
{
    /// <summary>
    /// Generic class used to create, modify, and retrieve entities.
    /// </summary>
    /// <remarks>
    /// This code is adapted from:
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </remarks>
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext entities;
        private readonly IDbSet<T> dbSet;

        /// <summary>
        /// Constructor that takes a DbContext dependency.
        /// </summary>
        public GenericRepository(DbContext entities)
        {
            this.entities = entities;
            dbSet = entities.Set<T>();
        }

        /// <inheritdoc cref="IRepository{T}.GetAll"/>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            // TODO: Use predicate expression when needed.
            //return predicate != null 
            //    ? dbSet.Where(predicate) 
            //    : dbSet.AsQueryable();

            return dbSet.AsQueryable();
        }

        /// <inheritdoc cref="IRepository{T}.Add"/>
        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Cannot add a null entity");
            }

            dbSet.Add(entity);
        }

        /// <inheritdoc cref="IRepository{T}.Remove"/>
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Cannot add a null entity");
            }

            dbSet.Remove(entity);
        }
    }
}
