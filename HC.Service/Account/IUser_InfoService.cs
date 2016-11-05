using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Service.Account
{
    public interface IUser_InfoService
    {

        #region  BasicMethod

        /// <summary>
        /// 判断用户名称是否存在
        /// </summary>
        /// <param name="p_username">用户名</param>
        /// <returns></returns>
        bool Exists(string p_username);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <returns></returns>
        User_Info authentiUser(string uName, string uPwd);

        /// <summary>
        /// 获取用户信息(根据用户名获取)
        /// </summary>
        /// <param name="p_trantype">ByName,ByID; </param> 
        /// <param name="P_Para">Name or ID</param>
        /// <returns></returns>
        User_Info GetUserInfo(Maticsoft.Model.User_Info_Search model);
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="iAll"></param>
        /// <returns></returns>
        DataTable GetUserInfoList(Maticsoft.Model.User_Info_Search model, out int iAll);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.User_Info GetUserInfo(DataRow row);

        /// <summary>
        /// 增加新用户（个人）
        /// </summary>
        /// <param name="model"></param>
        int Add(Maticsoft.Model.User_Info model);

        /// <summary>
        /// 插入企业用户信息
        /// </summary>
        /// <param name="modelExp"></param>
        /// <param name="modelUser"></param>
        /// <returns></returns>
        //int AddExp(Maticsoft.Model.User_Exp modelExp, Maticsoft.Model.User_Info modelUser);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        int Update(Maticsoft.Model.User_Info model);

        /// <summary>
        /// 更新商户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //int UpdateExp(Maticsoft.Model.User_Exp model);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="p_Id"></param>
        /// <param name="p_UserPwd"></param>
        /// <param name="p_UpdateBy"></param>
        /// <returns></returns>
        int UpdatePassword(int p_Id, string p_UserPwd, int p_UpdateBy);
        #endregion  BasicMethod


        /// <summary>
        /// 是否是商户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool GetIsCommercial(int userId);
    }
}
