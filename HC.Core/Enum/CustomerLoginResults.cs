using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



/*DateTime: 2015-05-18 ,createBy:FuJia*/
namespace HC.Core.Enum
{
    public enum CustomerLoginResults : int
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        Successful = 1,
        /// <summary>
        /// 账户信息不存在（用户名无效）
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// 密码错误
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// 帐户尚未激活(已被冻结)
        /// </summary>
        NotActive = 4
    }
}