using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HCHEv2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            routes.MapRoute(
            "Lists",
       "{controller}/{action}/{id}/{page}",
       new[] { "HCHEv2.Controllers" }
         );



            routes.MapRoute(
           "Search",
      "{controller}/{action}/{sKey}/{page}/{palcehold}",
      new[] { "HCHEv2.Controllers" }
        );

            routes.MapRoute(
                "Default",
           "{controller}/{action}/{id}/{*catchall}",
           new { controller = "Home", action = "Index", id = UrlParameter.Optional },
           new[] { "HCHEv2.Controllers" }
             );

        }
    }
}