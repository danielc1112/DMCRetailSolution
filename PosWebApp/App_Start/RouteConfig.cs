using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PosWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Sell",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sell", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Products",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Sales",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Sales", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaleDetails",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SaleDetails", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
