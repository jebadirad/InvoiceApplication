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
                url: "routes/User",
                defaults: new { controller = "Routes", action = "UserList" });
            routes.MapRoute(
                name: "GetUserList",
                url: "User/GetUserList",
                defaults: new { controller = "User", action = "GetUserList" }
                );
            
            routes.MapRoute(
                name: "Invoice",
                url: "routes/InvoiceList",
                defaults: new { controller = "Routes", action = "InvoiceList" });
            //api end point
            routes.MapRoute(
                name: "AddUser",
                url: "User/Add",
                defaults: new { controller = "User", action = "Add" }
                );
            //view
            routes.MapRoute(
              name: "AddUserForm",
              url: "routes/User/Add",
              defaults: new { controller = "Routes", action = "AddForm" });
            //api endpoint
            routes.MapRoute(
                name: "GetUser",
                url: "User/Get/{ID}",
                defaults: new { controller = "User", action = "Get", ID = UrlParameter.Optional }
                );
           

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" });
            
        }
    }
}
