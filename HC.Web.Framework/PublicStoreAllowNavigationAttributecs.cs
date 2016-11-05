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
    /// <summary>
    /// 自定义FilterAttribute(公共站点导航属性)
    /// </summary>
    public class PublicStoreAllowNavigationAttributecs : ActionFilterAttribute
    {
        string ralUrl = "";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
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

            if (filterContext.IsChildAction)
                return;

            //加入对图片上传的信任过滤
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(imgFileterAttribute), true);
            if (attrs.Length == 1)
            {
                return;
            }

           

            #region 原来form认证时用的
            var _authticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            object isAuthen = null;
            //判断是否通过验证
            isAuthen = _authticationService.GetAuthenticatedCustomer("placeHold");
            if (isAuthen is Maticsoft.Model.User_Info)
            {
                filterContext.Controller.ViewBag.curentUser = (Maticsoft.Model.User_Info)isAuthen;
            }
            #endregion
            if (((isAuthen is String) || isAuthen == null) &&
                //确认不是登录页
                (!controllerName.Equals("HCHEv2.Controllers.AccountController", StringComparison.InvariantCultureIgnoreCase) && !actionName.Equals("Login", StringComparison.InvariantCultureIgnoreCase)) &&
                //确认不是注销页
                (!controllerName.Equals("HCHEv2.Controllers.AccountController", StringComparison.InvariantCultureIgnoreCase) && !actionName.Equals("LogOff", StringComparison.InvariantCultureIgnoreCase)) &&
                //确认不是注册页
                (!controllerName.Equals("HCHEv2.Controllers.AccountController", StringComparison.InvariantCultureIgnoreCase) && !actionName.Equals("Register", StringComparison.InvariantCultureIgnoreCase)) &&
                //确认不是密码找回页--预留
                (!controllerName.Equals("HCHEv2.Controllers.AccountController", StringComparison.InvariantCultureIgnoreCase) && !actionName.Equals("PasswordRecovery", StringComparison.InvariantCultureIgnoreCase)) &&
                (!controllerName.Equals("HCHEv2.Controllers.AccountController", StringComparison.InvariantCultureIgnoreCase) && !actionName.Equals("PasswordRecoveryConfirm", StringComparison.InvariantCultureIgnoreCase))
                )
            {
                if (isAuthen == null)
                {
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
                }
            }
        }
    }


    /// <summary>
    /// 将此属性加在需要处理HttpUnauthorizedResult的action上 当该action处理返回的是HttpUnauthorizedResult的话 则会走下面的流程
    /// </summary>
    public class PblicStoreAuthorizeAtt : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult(
                    string.Concat(FormsAuthentication.LoginUrl,
                                 "?ReturnUrl=",
                                 filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri)));
            }
        }
    }
}



