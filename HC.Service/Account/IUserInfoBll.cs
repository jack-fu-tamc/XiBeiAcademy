using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Account
{
    public interface IUserInfoBll
    {
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="p_type">user type (use/exp)</param>
        /// <param name="p_Where">条件</param>
        /// <param name="iPageCount">页数</param>
        /// <param name="iPageSize">页面大小</param>
        /// <returns></returns>
        DataTable GetUserInfoList(string p_type, string p_Where, int iPageCount, int iPageSize, out int iAll);

        /// <summary>
        /// 获取用户信息(根据用户名获取)
        /// </summary>
        /// <param name="p_trantype">ByName,ByID; </param>
        /// <param name="P_Para">Name or ID</param>
        /// <returns></returns>
        Maticsoft.Model.User_Info GetUserInfo(string p_trantype, string P_Para);

        /// <summary>
        /// 增加 新用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns>新用户ID</returns>
        int Add(Maticsoft.Model.User_Info model);

    }
}
