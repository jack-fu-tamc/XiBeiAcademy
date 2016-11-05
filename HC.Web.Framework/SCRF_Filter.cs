using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HC.Core.Infrastructure;
using HC.Service.Authentication;
using System.Text.RegularExpressions;
using HC.Core.CommonMethod;


namespace HC.Web.Framework
{
    public class SCRF_Filter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
               

                var csrfName = filterContext.RequestContext.HttpContext.Request.UrlReferrer;
                var csrfNameSet = HC.Core.CommonMethod.UntilMethod.getAppSettingValue("csrfName");
                var csrfNameSetOther = HC.Core.CommonMethod.UntilMethod.getAppSettingValue("csrfNameOther");
                if (csrfName == null || (csrfName.Host.Trim() != csrfNameSet.Trim() && csrfName.Host != csrfNameSetOther.Trim()))
                {
                    #region 非法跳转
                    var redictUrl = string.Concat(FormsAuthentication.LoginUrl,
                            "?ReturnUrl=",
                            filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri));
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult()
                        {
                            Data = new
                            {
                                result = "1",
                                LogOnUrl = string.Concat(FormsAuthentication.LoginUrl,
                              "?ReturnUrl=",
                              filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.UrlReferrer.AbsolutePath))
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult(redictUrl);
                    }


                    #endregion


                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
