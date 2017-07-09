using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InsuranceTestApp.Repositories
{
    /// <summary>
    /// Generic unit of work used to interact with repositories.
    /// </summary>
    /// <remarks>
    /// This code is adapted from:
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </remarks>
    public class GenericUnitOfWork : IUnitOfWork
    {
        private readonly DbContext entities = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GenericUnitOfWork()
        {
            entities = new ApplicationDbContext();
            Repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Internal constructor used to inject a database context dependency.
        /// </summary>
        /// <remarks>Exposed internally to the unit test project.</remarks>
        /// <param name="dbContext">Database context to use.</param>
        internal GenericUnitOfWork(DbContext dbContext)
        {
            entities = dbContext;
            Repositories = new Dictionary<Type, object>();
        }

        /// <inheritdoc cref="IUnitOfWork.Repositories"/>
        public Dictionary<Type, object> Repositories { get; }

        /// <inheritdoc cref="IUnitOfWork.Repository{T}"/>
        public IRepository<T> Repository<T>() where T : class
        {
            IRepository<T> repo;
            if (Repositories.Keys.Contains(typeof(T)))
            {
                repo = Repositories[typeof(T)] as IRepository<T>;
            }
            else
            {
                repo = new GenericRepository<T>(entities);
                Repositories.Add(typeof(T), repo);
            }

            return repo;
        }

        /// <inheritdoc cref="IUnitOfWork.SaveChanges"/>
        public void SaveChanges()
        {
            entities.SaveChanges();
        }

        private bool disposed = false;

        /// <inheritdoc cref="IDisposable.Dispose"/>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
