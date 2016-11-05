using System;
using System.IO;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.Routing;
using HC.Core.Infrastructure;

namespace HC.Web.Framework.UI.Captcha
{
    public static class HtmlExtensions
    {
    
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="fun"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="hint"></param>
        /// <returns></returns>
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper helper, string id, string fun, object htmlAttributes, string url=null, string hint = null)
        {
            // <img id="m_imgCaptcha" alt="Click to Change image" src="" title="Click to Change image"  onclick="loadCaptcha();" style="width: 200px; height: 50px;" />
            // Create tag builder
            var builder = new TagBuilder("img");
            builder.GenerateId(id);
            builder.MergeAttribute("src", url);
            //hint:换一组验证码！
            builder.MergeAttribute("alt", hint);
            builder.MergeAttribute("title", hint);
            //fun:loadCaptcha();
            builder.MergeAttribute("onclick", fun);
            //HtmlHelper.AnonymousObjectToHtmlAttributes
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return id.Replace('[', '_').Replace(']', '_');
        }

    }
}
