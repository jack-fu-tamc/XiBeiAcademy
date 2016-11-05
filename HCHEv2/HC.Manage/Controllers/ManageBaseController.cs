using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Web.Framework.UI;
using HC.Web.Framework;
using HC.Web.Framework.Security;
using HC.Core.Infrastructure;
using HC.Service.OpLog;
using HC.Data.Models;


namespace HC.Manage.Controllers
{
    [SCRF_Filter]
    [HCHttpsRequirementAttribute(SslRequirement.Yes)]
    [PublicStoreAllowNavigationAttributecs]
    [ManageStoreAllowNavigationAttributecs]
    [noMoble]
    public class ManageBaseController:Controller
    {
        /// <summary>
        /// 显示成功通知
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }

        /// <summary>
        /// 显示失败通知
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("HC.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        protected virtual void AddOpLog(string desc)
        {
            var _opLogService = EngineContext.Current.Resolve<IopLogService>();
            var isAuthenUser = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var entity = new OpLog();
            entity.UserID = isAuthenUser.Id;
            entity.Id = System.Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.OpDescriptions = desc;
            entity.OpResult = "成功";
            entity.UserAccount = isAuthenUser.UserName;
            entity.UserRealName = isAuthenUser.RealName;
            _opLogService.AddOpLog(entity);
        } 
    }
}