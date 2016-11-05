using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HC.Web.Framework.Mvc;
using HC.Core.Infrastructure;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using HC.Web.Framework.Themes;
using RouteDebug;
using log4net;
using ResposityOfEf;
using System.Data.Entity;
using System.Collections;
namespace HCHEv2
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //预留
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            //初始化日志log4net
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\applicationLog.config"));


            //引擎初始化
            EngineContext.Initialize(false);
            //设置引擎
            var dependencyResolver = new HCDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);


            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            BundleTable.EnableOptimizations = true;
            Database.SetInitializer<ResposityOfEf.ErpDataContext>(null);

        }



        protected void Application_EndRequest(object seder, EventArgs e)
        {

        }


        protected void Session_Start(object sender, EventArgs e)
        {
            //Response.Cookies["ASP.NET_SessionId"].HttpOnly = true;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Hashtable hOnline = (Hashtable)Application["Online"];
            if (hOnline != null)
            {
                if (hOnline[Session.SessionID] != null)
                {
                    hOnline.Remove(Session.SessionID);
                    Application.Lock();
                    Application["Online"] = hOnline;
                    Application.UnLock();
                } 
            }
           
        }

    }
}