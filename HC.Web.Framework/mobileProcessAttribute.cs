using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HC.Web.Framework
{
    [AttributeUsage(AttributeTargets.All)]
    public class mobileProcessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

          
            //object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(noMobleAttribute), true);
            var controllerAttrs = filterContext.ActionDescriptor.ControllerDescriptor.GetFilterAttributes(false);
            var noMobileFlag= controllerAttrs.Any(x => x.GetType() == typeof(noMobleAttribute));
            if (!noMobileFlag)
            {
                var para = filterContext.ActionDescriptor.GetParameters();
                var typeList = new List<Type>(); 
                if (para.Length > 0)
                {
                    for (int i = 0; i < para.Length; i++)
                    {
                        typeList.Add(para[i].ParameterType);
                    }
                }

                //var cc = filterContext.Controller.GetType();
                //var bb = cc.GetMethod(filterContext.ActionDescriptor.ActionName, tt);
                
                var actionMaethod = filterContext.Controller
                    .GetType().GetMethod(filterContext.ActionDescriptor.ActionName, typeList.ToArray());

                //if (actionMaethod.ReturnType == typeof(ContentResult)||actionMaethod.ReturnType==typeof(JsonResult))
                //{
                //    //ViewResult
                //}

                var originActionName = filterContext.RouteData.Values["action"];
                var reType = actionMaethod.ReturnType.Name; //== null ? "ActionResult" : actionMaethod.ReflectedType.Name;

                //var ty = actionMaethod.ReturnType;
                //var da = filterContext.Controller.ViewData;

                //var ccc = filterContext.ActionDescriptor.GetParameters();
                // var bb=filterContext.Controller.ValueProvider.GetValue();

                if ((filterContext.HttpContext.Request.Browser.IsMobileDevice || filterContext.HttpContext.Request.UserAgent.ToLower().Contains("micromessenger")) && (reType != "JsonResult" && reType != "ContentResult"))
                {
                    //var originActionName = filterContext.RouteData.Values["action"];

                    if (filterContext.Controller.ViewBag.MViewName != null)
                    {
                        filterContext.Result = new ViewResult()
                        {
                            ViewName = "m" + filterContext.Controller.ViewBag.MViewName,
                            ViewData = filterContext.Controller.ViewData
                        };
                    }
                    else
                    {
                        filterContext.Result = new ViewResult()
                        {
                            ViewName = "m" + originActionName,
                            ViewData = filterContext.Controller.ViewData
                        };
                    }

                    
                }
               

                base.OnActionExecuted(filterContext);
            }
        }
    }
}
