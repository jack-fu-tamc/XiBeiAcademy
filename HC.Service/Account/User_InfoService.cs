using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResposityOfEf;
using HC.Data.Models;
using Maticsoft.Model;

namespace HC.Service.Account
{
    public class User_InfoService:IUser_InfoService
    {
        private IRepository<CxUser> _userRepository;

        public User_InfoService(IRepository<CxUser> userRepository)
        {
            this._userRepository = userRepository;
        }
        public bool Exists(string p_username)
        {
            return true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <returns></returns>
        public User_Info authentiUser(string uName,string uPwd)
        {
            var query = _userRepository.Table;
            var result = query.Where(x => x.UserName == uName && x.UserPassword == uPwd).FirstOrDefault();
            if ( result!= null)
            {
                return new User_Info() { Address = result.Address, Email = result.Email, Id = result.UserID, isAdmin = result.isAdmin, Mobile = result.Mobile, RealName = result.RealName, UserName = result.UserName, UserPwd = result.UserPassword, effctive=result.Effective, passWordTime=result.passWordTime };
            }
            else
            {
                return null;
            }
        }


        public User_Info GetUserInfo(Maticsoft.Model.User_Info_Search model)
        {
            var query = _userRepository.Table;
            CxUser userR;
            if(model.trantype=="ByName")
             userR= query.Where(x => x.UserName == model.para).ToList().FirstOrDefault();
            else
                userR = query.Where(x => x.UserID ==int.Parse(model.para)).ToList().FirstOrDefault();
            var result = new Maticsoft.Model.User_Info();


            result.UserName = userR.UserName;
            result.RealName = userR.RealName;
            result.Email = userR.Email;
            result.Address = userR.Address;
            result.Mobile = userR.Mobile;
            result.Id = userR.UserID;
            result.UserPwd = userR.UserPassword;
            result.isAdmin = userR.isAdmin;
            
            return result;
        }

        public System.Data.DataTable GetUserInfoList(Maticsoft.Model.User_Info_Search model, out int iAll)
        {
            iAll = 0;
            return null;
        }

        public Maticsoft.Model.User_Info GetUserInfo(System.Data.DataRow row)
        {
            return null;
        }

        public int Add(Maticsoft.Model.User_Info model)
        {
            return 1;
        }

        public int Update(Maticsoft.Model.User_Info model)
        {
            return 1;
        }

        public int UpdatePassword(int p_Id, string p_UserPwd, int p_UpdateBy)
        {
            return 1;
        }

        public bool GetIsCommercial(int userId)
        {
            return true;
        }
    }
}
