using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace HC.Web.Framework.ueditor
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public class ConfigHandler : Handler
    {
        public ConfigHandler(HttpContext context) : base(context) { }

        public override void Process()
        {
            WriteJson(Config.Items);
        }
    }
}