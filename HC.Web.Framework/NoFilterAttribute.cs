using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HC.Web.Framework
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NoFilterAttribute : ActionFilterAttribute
    {

    }

    
    [AttributeUsage(AttributeTargets.Method)]
    public class imgFileterAttribute:ActionFilterAttribute
    {

    }
}
