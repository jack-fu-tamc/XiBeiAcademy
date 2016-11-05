using System;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HC.Data.ViewModels
{
    public class WebSiteViewModel
    {
      
        /// <summary>
        /// 密码更改周期(小时)
        /// </summary>
        public int overdueHoru { get; set; }
        /// <summary>
        /// https配置
        /// </summary>
        public bool OpenSSl { get; set; }
        /// <summary>
        /// 登录验证码开关
        /// </summary>
        public bool verificationCode { get; set; }

        /// <summary>
        /// 登录锁定次数
        /// </summary>
        public int lockLogin { get; set; }

        /// <summary>
        /// 账号锁定时长
        /// </summary>
        public int lcokTimeLenth { get; set; } 

        /// <summary>
        /// 密码长度
        /// </summary>
        public int passWordLength { get; set; }
    }
}
