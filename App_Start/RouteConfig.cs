using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InvoiceApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Users",
                url: "routes/AddForm",
                defaults: new { controller = "Routes", action = "AddForm" });

            routes.MapRoute(
                name: "Pages",
                url: "routes/PageList",
                defaults: new { controller = "Routes", action = "PageList"});

            routes.MapRoute(
                name: "Invoice",
                url: "routes/InvoiceList",
                defaults: new { controller = "Routes", action = "InvoiceList" });

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}
