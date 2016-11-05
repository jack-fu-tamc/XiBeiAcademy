using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace HC.Web.Framework
{
    public class monitoringAction : ActionFilterAttribute
    {
        public DateTime AbeginTime;
        public DateTime AendTime;
        public DateTime RbeginTime;
        public DateTime RendTime;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            AbeginTime = System.DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            AendTime = System.DateTime.Now;
        }



        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            RbeginTime = System.DateTime.Now;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            RendTime = System.DateTime.Now;
            var RthroughTime = RendTime - RbeginTime;
            var AthroughTime = AendTime - AbeginTime;
            
            filterContext.HttpContext.Response.Write(string.Format("<div style='position: relative;z-index: 1000;bottom: 0;left: 0;color: red;'>{0}razor执行时间:{1}   {0}action执行时间:{2}</div>" , filterContext.RouteData.Values["action"],RthroughTime.ToString(), AthroughTime.ToString()));//会追加到response流的末尾
        }
    }
}
