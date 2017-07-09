using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InsuranceTestApp.Repositories;

namespace InsuranceTestApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

#if DEBUG
            // Re-create the database when the underlying model changes.
            // Use database migrations for production/release code.
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
#endif
        }
    }
}
