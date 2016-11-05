using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HC.Manage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);



            //routes.MapRoute(
            //   "Manage_evaluate",
            //    "Myevaluate/{id}",
            //    new { controller = "evaluate", action = "Myevaluate", area = "Manage", id = UrlParameter.Optional }
            //    //new[] { "HC.Manage.Controllers" }
            //   );



          


            //routes.MapRoute(//这里不指定namespace是因为访问的时候要在前面加manage
            //    name: "alipay",
            //    url: "ReadAliPay/{palacHold}",
            //    defaults: new { controller = "Alipay", action = "readLog", palacHold = UrlParameter.Optional },
            //    namespaces: new[] { "HC.Manage.Controllers" }
            //    );
        }
    }
}