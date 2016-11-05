using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Web.Framework;
using HC.Web.Framework.UI;
using HC.Core;

namespace HCHEv2.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [PublicStoreAllowNavigationAttributecs]
    public class HcBaseController : Controller
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

    }
}
