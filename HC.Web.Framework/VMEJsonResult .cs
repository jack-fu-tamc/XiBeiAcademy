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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace HC.Web.Framework
{
    /// <summary>  
    /// 通过重载ExecuteResult方法，实现自定义序列化日期的实现  
    /// </summary>  
    public class VMEJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (this.Data != null)
            {
           
                // 设置日期序列化的格式  
               
                JsonSerializerSettings jsSettings = new JsonSerializerSettings();
                jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsSettings.Converters.Add(new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy'-'MM'-'dd"//DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"                     
                });                
                response.Write(JsonConvert.SerializeObject(Data, jsSettings));
            }
        }
    }  
}
