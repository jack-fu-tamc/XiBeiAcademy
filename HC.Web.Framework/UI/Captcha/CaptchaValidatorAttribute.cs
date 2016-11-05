using System.Web.Mvc;
using HC.Core.Infrastructure;

namespace HC.Web.Framework.UI.Captcha
{
    // OnActionExecuted 在执行操作方法后调用。
    // OnActionExecuting 在执行操作方法之前调用。
    // OnResultExecuted 在执行操作结果后调用。
    // OnResultExecuting 在执行操作结果之前调用。

    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        /// 自定义验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool valid = false;       
           valid = Validation(filterContext);
            filterContext.ActionParameters["captchaValid"] = valid;

            base.OnActionExecuting(filterContext);
        }

        protected bool Validation(ActionExecutingContext filterContext)
        {
            bool IsValid = false;
            var captchaValue = filterContext.HttpContext.Request.Form["CaptchaCode"];//post来的text
            if (!filterContext.HttpContext.Session.IsNewSession && !string.IsNullOrEmpty(captchaValue))
            {
                var sessionValue = filterContext.HttpContext.Session["captchaText"].ToString();//生成图片的text
                if ((captchaValue != null) && (captchaValue.ToLower() == sessionValue.ToLower()))
                {
                    //在这里可以做跳转处理
                    IsValid = true;
                }
            }
            return IsValid;
        }

    }
}
