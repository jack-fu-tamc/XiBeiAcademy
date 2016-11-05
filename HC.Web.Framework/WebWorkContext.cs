using System;
using System.Collections.Generic;
using System.Linq;
using HC.Core;
using Maticsoft.Model;
using System.Web;
using HC.Service.User;
using HC.Service.Account;
using HC.Service.Authentication;

namespace HC.Web.Framework
{
    public partial class WebWorkContext : IWorkContext
    {

        #region filed
        private readonly HttpContextBase _httpContext;
        private const string CustomerCookieName = "HC.AUTH";
        private User_Info _cachedCustomer; 
        private IUser_InfoService _customService;
        private IAuthenticationService _authenticationService;
        #endregion

        #region cto
        public WebWorkContext(HttpContextBase httpContext, IUser_InfoService CustomService, IAuthenticationService authenticationService)
        {
            this._httpContext = httpContext;
            this._customService = CustomService;
            this._authenticationService = authenticationService;
           
        }
        #endregion


        public User_Info GetCurrentUser()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;

            User_Info Customer = null;
            if (_httpContext != null)
            {
                if (Customer == null)
                {
                    Customer = _authenticationService.GetAuthenticatedCustomer();//根据票证获取customer
                }

                //预留 可以给Cookie中存些信息
                //if (Customer == null )
                //{
                //    var userCookie = GetCustomerCookie();
                //    if (userCookie != null && !String.IsNullOrEmpty(userCookie.Value))
                //    {   
                //        string userName=userCookie.Value;

                //            var userByCookie = _customService.GetCustomerByUsername(userName);
                //            if (userByCookie != null)
                //                Customer = userByCookie;  
                //    }
                //}
                //if (Customer != null)
                //    SetCustomerCookie(Customer.UserName);


                _cachedCustomer = Customer;

            }
            return _cachedCustomer;
        }

        public User_Info CurrentCustomer
        {
            get
            {
                //if (_cachedCustomer != null)
                //    return _cachedCustomer;
                //return null;
                return GetCurrentUser();
            }
            set
            {
                //SetCustomerCookie(value.UserName); 
                _cachedCustomer = value;
            }

        }

        public bool GetIsCommercial()
        {
            //var currentCostom = GetCurrentUser();
            //if (currentCostom != null)
            //{
            //    //return _customService.get(currentCostom.Id);
            //    var curenExpUser = _Iuser_ExpService.GetShopInfoByUserID(currentCostom.Id);
            //    if (curenExpUser == null )
            //    {
            //        return false;
            //    }
            //    else if(curenExpUser.Expconf == 2)//只要审核通过就放行进入后台 然后根据类型去不同的后台//0917
            //    //else if ((curenExpUser.ExpType == 3 || curenExpUser.ExpType == 4)&&curenExpUser.Expconf==2)//0908  8楼只允许4S店和汽保服务商可进店铺中心
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }

        protected void SetCustomerCookie(string customerName)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(CustomerCookieName);
                cookie.HttpOnly = true;
                cookie.Value = customerName;
                if (customerName == "")
                {
                    cookie.Expires = DateTime.Now.AddMonths(-1);
                }
                else
                {
                    int cookieExpires = 60 * 1; //TODO make configurable
                    //cookie.Expires = DateTime.Now.AddHours(cookieExpires);
                    cookie.Expires = DateTime.Now.AddMinutes(cookieExpires);
                }

                _httpContext.Response.Cookies.Remove(CustomerCookieName);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        protected HttpCookie GetCustomerCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[CustomerCookieName];
        }

        /// <summary>
        /// 是否是商户
        /// </summary>
        public bool IsCommercial
        {
            get
            {
                return GetIsCommercial();
            }
        }
    }
}
