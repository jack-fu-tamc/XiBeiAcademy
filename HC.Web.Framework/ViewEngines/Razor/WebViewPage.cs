using System;
using System.IO;
using System.Web.Mvc;
using System.Web.WebPages;

using HC.Core;
using HC.Core.Infrastructure;
using HC.Web.Framework.Themes;

namespace HC.Web.Framework.ViewEngines.Razor
{
    /// <summary>
    /// 自定义 Razor 视图的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class WebViewPage<TModel>:System.Web.Mvc.WebViewPage<TModel>
    {
        private IWorkContext _workContext;
        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }
        public override string Layout
        {
            get
            {
                var layout = base.Layout;
                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }

    /// <summary>
    /// 前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
