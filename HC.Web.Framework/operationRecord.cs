using System;
using System.Web.Mvc;
using HC.Core;
using HC.Core.Infrastructure;
using HC.Data.Models;
using HC.Service.OpLog;

namespace HC.Web.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class operationRecord : ActionFilterAttribute
    {
        public IopLogService _opLogService;
        public string opName { get; set; }
        public operationRecord(string opName)
        {
            this.opName = opName;
            this._opLogService = EngineContext.Current.Resolve<IopLogService>();
        }

        //public operationRecord()
        //{
        //   this._opLogService= EngineContext.Current.Resolve<IopLogService>();
        //}


        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            var isAuthenUser = (Maticsoft.Model.User_Info)filterContext.Controller.ViewBag.curentUser;
            var entity = new OpLog();
            entity.UserID = isAuthenUser.Id;
            entity.Id = System.Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.OpDescriptions = opName;//+filterContext.Controller.ControllerContext.RouteData.Values[""]
            entity.OpResult = "成功";
            entity.UserAccount = isAuthenUser.UserName;
            entity.UserRealName = isAuthenUser.RealName;
            _opLogService.AddOpLog(entity);
        }

        
    }
}
