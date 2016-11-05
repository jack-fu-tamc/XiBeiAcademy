using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hche.Common.Enum
{
    public class SortEnum
	{
        public enum sortClass
        {
           
            newsCheck = 1,  //新闻审核 
            analysis = 2, //统计分析
            userManage = 3, //用户管理
            webSet=4,//站点设置
            newsPromptly=5,//新闻直发
            logRecord=6,//日志记录
            recycle=7,//回收站
            newsManage = 8,//常规管理
            AcademySys=9//报名系统
        }
	}
}