using System;
using System.Collections.Generic;

namespace InsuranceTestApp.Repositories
{
    /// <summary>
    /// Interface for a generic unit of work used to interact with repositories.
    /// </summary>
    /// <remarks>
    /// This code is adapted from:
    /// https://www.codeproject.com/Articles/770156/Understanding-Repository-and-Unit-of-Work-Pattern
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Stores a collection of repositories. Used to ensure a single repository is used per object, per unit of work.
        /// </summary>
        Dictionary<Type, object> Repositories { get; }

        /// <summary>
        /// Used to retrieve a repository for the specified type of entity./>
        /// </summary>
        /// <typeparam name="T">Type of entity stored in the repo.</typeparam>
        /// <returns>A repository for the specified type of entity.</returns>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// Saves any changes to the repos.
        /// </summary>
        void SaveChanges();
    }
}
