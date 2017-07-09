using System;
using System.Linq;
using System.Linq.Expressions;

namespace InsuranceTestApp.Repositories
{
    /// <summary>
    /// Generic interface for repositories.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves entities from the repo.
        /// </summary>
        /// <param name="predicate">Function describing what entities to retrieve.</param>
        /// <returns>A collection of repo entities.</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Adds an entity to the repo.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Removes an entity from the repo.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        void Remove(T entity);
    }
}
