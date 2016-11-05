using System.Web;
using System.Web.Mvc;
using HC.Web.Framework;

namespace HCHEv2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new publicLognetAttribute());
            //filters.Add(new publicLognetAttribute());
            filters.Add(new mobileProcessAttribute());
        }
    }
}