using System.Web;
using System.Web.Mvc;
using HC.Web.Framework;

namespace HC.Manage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new publicLognetAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}