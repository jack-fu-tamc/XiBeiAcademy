using System.Web.Compilation;
using System.Web.Mvc;

namespace HC.Web.Framework.Themes
{
    public abstract class ThemeableBuildManagerViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        /// <summary>
        /// 返回一个值，该值使用指定的控制器上下文来指示文件是否位于指定的路径中。
        /// </summary>
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return BuildManager.GetObjectFactory(virtualPath, false) != null;
        }
    }
}
