using SistemaFila.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SistemaFila
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SistemaFilaContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<SistemaFilaContext>());
            //Database.SetInitializer(new SistemaFilaInitializer());
        }
    }
}
