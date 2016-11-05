using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.Model;

namespace HC.Core
{
    public interface IWorkContext
    {
        /// <summary>
        /// 获取和设置当前客户
        /// </summary>
        User_Info CurrentCustomer { get; set; }

        /// <summary>
        /// 用户状态(正常,待审核,驳回，冻结 userinfo!=null userExp!=null  expuser.Expconf==2则为true)
        /// </summary>
        bool IsCommercial { get; }
    }
}
