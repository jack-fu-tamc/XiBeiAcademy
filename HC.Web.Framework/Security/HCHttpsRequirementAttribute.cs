using System;
using System.Web.Mvc;
using HC.Core;
using HC.Core.Infrastructure;
using HC.Core.CommonMethod;

namespace HC.Web.Framework.Security
{
    /// <summary>
    /// 是否需要验证ssl过滤器（预留扩展暂不实现）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,Inherited=true,AllowMultiple=true)]
    public class HCHttpsRequirementAttribute : FilterAttribute,IAuthorizationFilter
    {
        public HCHttpsRequirementAttribute(SslRequirement sslRequirement)
        {

            if (Convert.ToBoolean(UntilMethod.getAppSettingValue("OpenSSl")))
            {
                this.SslRequirement = SslRequirement.Yes;
            }
            else
            {
                this.SslRequirement = SslRequirement.NoMatter;
            }


            //this.SslRequirement = sslRequirement;
        }


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            if (filterContext.HttpContext.Request.Url.ToString().IndexOf("https") > -1)
            {
                return;
            }
            //忽略所有ChildAction
            if (filterContext.IsChildAction)
                return;

           //只对GET过滤
            if (!String.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

      


            switch (this.SslRequirement)
            {
                case SslRequirement.Yes:
                    {
                       
                            
                                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                                //redirect to HTTPS version of page
                                //string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                                string url = webHelper.GetThisPageUrl(true, true);
                                
                                filterContext.Result = new RedirectResult(url);
                    }
                    break;
                case SslRequirement.NO:
                    {
                        
                            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

                            //redirect to HTTP version of page
                            //string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                            string url = webHelper.GetThisPageUrl(true, false);
                            filterContext.Result = new RedirectResult(url);
                       
                    }
                    break;
                case SslRequirement.NoMatter:
                    {
                        //do nothing
                    }
                    break;
                default:
                    throw new HCException("Not supported SslProtected parameter");
            }
        }

        public SslRequirement SslRequirement { get; set; }
    }
}
