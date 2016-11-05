using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HC.Service.Account
{
    public interface AccountBll
    {
        //DAL.AccountInfo ai = new AccountInfo();
        //#region userinfo
        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <param name="p_trantype">ByName,ByID; </param>
        ///// <param name="P_Para">Name or ID</param>
        ///// <returns>用户信息（普通用户）</returns>
        //public DataTable GetUserInfoList(string p_trantype, string P_Para)
        //{
        //    DataTable dt = new DataTable();
        //    dt = ai.GetUserInfoList(p_trantype, P_Para);
        //    return dt;
        //}
        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <param name="p_trantype">ByName,ByID; </param>
        ///// <param name="P_Para">Name or ID</param>
        ///// <returns>用户对象实体（普通用户）</returns>
        //public Maticsoft.Model.Acc_Info GetUserInfo(string p_trantype, string P_Para)
        //{
        //    Maticsoft.Model.Acc_Info model = new Maticsoft.Model.Acc_Info();
        //    model = ai.GetUserInfo(p_trantype, P_Para);
        //    return model;
        //}
        //#endregion

        #region User_DeliverAddress
        #endregion

        #region User_Complaints

        #endregion

        #region user phone

        #endregion


        #region User_Payment

        #endregion
    }
}
