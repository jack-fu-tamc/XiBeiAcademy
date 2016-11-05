using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Core.Infrastructure;
using HC.Service.Authentication;
using HC.Core;

namespace HC.Web.Framework
{
    public class ManageStoreAllowNavigationAttributecs : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // base.OnActionExecuting(filterContext);
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null)
                return;

            string actionName = filterContext.ActionDescriptor.ActionName;
            if (String.IsNullOrEmpty(actionName))
                return;

            string controllerName = filterContext.Controller.ToString();
            if (String.IsNullOrEmpty(controllerName))
                return;

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
                return;


            #region 加入对noFilter的信任过滤
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoFilterAttribute), true);
            if (attrs.Length == 1)
            {
                return;
            }
            #endregion


            #region 是否是商户验证 若否 则跳转至 无权限页面
            var workContext = EngineContext.Current.Resolve<IWorkContext>();//fujia+    
             
            var curentUser = workContext.CurrentCustomer;
            if (curentUser != null)
            {
                if (!curentUser.isAdmin)
                {
                   // filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary { { "controller", "Home" }, { "action", "deny" } });
                    filterContext.Result = new RedirectResult("/Home/deny");//0421+ jia
                    //filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "controller", "home" }, { "action", "NoCertification" }, { "area", "Manage" } });//0911 jia
                }
            }
            #endregion
        }
    }
}