using InsuranceTestApp.Models;
using System.Data.Entity;

namespace InsuranceTestApp.Repositories
{
    /// <summary>
    /// Database context class containing database sets for entities used by the web app.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Database set of policy entities.
        /// </summary>
        public virtual DbSet<Policy> Policies { get; set; }

        /// <summary>
        /// Database set of customers.
        /// </summary>
        public virtual DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Database set of risks.
        /// </summary>
        public virtual DbSet<Risk> Risks { get; set; }
    }
}