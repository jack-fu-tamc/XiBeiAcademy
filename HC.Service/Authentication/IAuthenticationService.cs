using System;
using Maticsoft.Model;

namespace HC.Service.Authentication
{
    public partial interface IAuthenticationService
    {
        void SignIn(User_Info customer);
        void SignOut();
        User_Info GetAuthenticatedCustomer();

        /// <summary>
        /// 注册成功后取得当前用户
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="pwd"></param>
        void registerSing(string uName, string pwd);

        object GetAuthenticatedCustomer(string placeholder);

        void getAuth(string linqp);
    }
}
