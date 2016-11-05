using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core.Common.logHelper
{
    public class LoggerHelper
    {
        static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");
        static readonly log4net.ILog logmonitor = log4net.LogManager.GetLogger("logmonitor");
        public static void Error(string ErrorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                var statck = ex.StackTrace;
                var sb = new StringBuilder();

                while (statck.IndexOf("位置") > -1)
                {
                    var indexStrat = statck.IndexOf("位置");

                    #region 截取指示 发生错误方法的所在位置
                    var atPstr=statck.Substring(0,indexStrat);
                    var atIndex = atPstr.LastIndexOf("在");
                    var atstr = atPstr.Substring(atIndex);
                    #endregion
                    sb.Append(atstr);
                   
                    #region 截取发生错误的位置行号
                    var indexEnd = statck.IndexOf("行号") + 7;
                    var substr = statck.Substring(indexStrat, indexEnd - indexStrat);
                    #endregion
                    sb.Append(substr);
                    statck = statck.Replace(substr, "");
                }
                //logerror.Error(ErrorMsg, ex);
                logerror.Error(ErrorMsg + "   " + "</br>" + sb.ToString());
            }
            else
            {
                logerror.Error(ErrorMsg);
            }
        }

        public static void Info(string Msg)
        {
            loginfo.Info(Msg);
        }

        public static void Monitor(string Msg)
        {
            logmonitor.Info(Msg);
        }
    }
}
