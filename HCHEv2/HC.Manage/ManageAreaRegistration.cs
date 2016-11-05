using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HC.Manage
{
    public class ManageAreaRegistration:AreaRegistration
    {
        public override string AreaName
        {
            get { return "Manage"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {



            context.MapRoute(
             "Manage_evaluate",
              "Manage/Myevaluate/{id}",
              new { controller = "evaluate", action = "Myevaluate", area = "Manage", id = UrlParameter.Optional }, 
              new[] { "HC.Manage.Controllers" }
             );

            context.MapRoute(
                "Manage_EnrolSys",
                "Manage/EnrolSys/{action}",
                new { controller = "EnrolSys", action = "Index", area = "Manage" },
                new[] { "HC.Manage.Controllers" }
                );

            context.MapRoute(
                 "Manage_default",
                 "Manage/{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", area = "Manage", id = "" },
                 new[] { "HC.Manage.Controllers" }
             );

            context.MapRoute(
                 "EditLanMu",
                 "Manage/{controller}/{action}/{ParentID}/{id}",
                //new { controller = "Home", action = "Index", area = "Manage", id = "" },
                 new[] { "HC.Manage.Controllers" }
                );
        }
    }
}