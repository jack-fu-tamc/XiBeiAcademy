using System;
using System.Web;
using System.Web.Security;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using HC.Service.User;
using Maticsoft.Model;
using System.Text.RegularExpressions;
using HC.Service.Account;
using HC.Core.CommonMethod;
using ResposityOfEf;
using System.Collections;


///PassPort认证相关服务 
///createBy-fujia
///2015-07-03
namespace HC.Service.Authentication
{
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        #region fild
        private readonly HttpContextBase _httpContext;
        private readonly IUser_InfoService _customerService;
        private readonly TimeSpan _expirationTimeSpan;
        private User_Info _cachedCustomer;
        #endregion

        #region ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="customerService">Customer service</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            IUser_InfoService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }
        #endregion

        public virtual void SignIn(User_Info customer)
        {
            //var now = DateTime.UtcNow.ToLocalTime();

            //#region 设置票据中的信息
            //var ticket = new FormsAuthenticationTicket(
            //    1 /*version*/,
            //     customer.UserName,
            //    now,
            //    now.Add(_expirationTimeSpan),
            //    false,
            //    customer.UserName,
            //    FormsAuthentication.FormsCookiePath);
            //var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //#endregion

            //#region 设置cookie过期时间
            //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //cookie.HttpOnly = true;
            //if (ticket.IsPersistent)
            //{
            //    cookie.Expires = ticket.Expiration;
            //}
            //cookie.Secure = FormsAuthentication.RequireSSL;
            //cookie.Path = FormsAuthentication.FormsCookiePath;
            //if (FormsAuthentication.CookieDomain != null)
            //{
            //    cookie.Domain = FormsAuthentication.CookieDomain;
            //}

            //_httpContext.Response.Cookies.Add(cookie);
            //_cachedCustomer = customer;
            //#endregion

            #region 保存session和清除前session
            #region old
            //for (int i = 0; i < _httpContext.Session.Count; i++)
            //{
            //    // var cc = _httpContext.Session.Keys[i] + ":" + _httpContext.Session[i].ToString();
            //    if (customer.UserName == _httpContext.Session[i].ToString())
            //    {
            //        _httpContext.Session.Remove(_httpContext.Session.Keys[i]);
            //        break;
            //    }
            //    // _httpContext.Session.SessionID
            //}

            //_httpContext.Session.Add(_httpContext.Session.SessionID, customer.UserName);
            #endregion


            try
            {
                Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                if (hOnline != null)
                {
                    int i = 0;
                    while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
                    {
                        IDictionaryEnumerator idE = hOnline.GetEnumerator();
                        string strKey = "";
                        while (idE.MoveNext())
                        {
                            if (idE.Value != null && idE.Value.ToString().Equals(customer.UserName))
                            {
                                //already login               
                                strKey = idE.Key.ToString();
                                //hOnline[strKey] = "XXXXXX";
                                hOnline.Remove(strKey);
                                break;
                            }
                        }
                        i = i + 1;
                    }
                }
                else
                {
                    hOnline = new Hashtable();
                }
                hOnline[_httpContext.Session.SessionID] = customer.UserName;
                _httpContext.Application.Lock();
                _httpContext.Application["Online"] = hOnline;
                _httpContext.Application.UnLock();
            }
            catch (Exception ex)
            {

            }

            //Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
            //if (hOnline != null)
            //{
            //    int i = 0;
            //    while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
            //    {
            //        IDictionaryEnumerator idE = hOnline.GetEnumerator();
            //        string strKey = "";
            //        while (idE.MoveNext())
            //        {
            //            if (idE.Value != null && idE.Value.ToString().Equals(customer.UserName))
            //            {
            //                //already login               
            //                strKey = idE.Key.ToString();
            //                //hOnline[strKey] = "XXXXXX";
            //                hOnline.Remove(strKey);
            //                break;
            //            }
            //        }
            //        i = i + 1;
            //    }
            //}
            //else
            //{
            //    hOnline = new Hashtable();
            //}
            //hOnline[_httpContext.Session.SessionID] = customer.UserName;
            //_httpContext.Application.Lock();
            //_httpContext.Application["Online"] = hOnline;
            //_httpContext.Application.UnLock();


            #endregion




            var now = DateTime.UtcNow.ToLocalTime();

            #region 生成本地票据


            #region 获取当前会话sessionID  并存入ticket中
            var currentSession_ID = _httpContext.Session.SessionID;
            #endregion


            #region 设置票据中的信息
            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                 customer.UserName,
                now,
                now.Add(_expirationTimeSpan),
                false,//持久化
                string.Format("{0}:{1}", customer.Id.ToString(), currentSession_ID),
                FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            #endregion

            #region 设置cookie过期时间
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            //cookie.Domain = ".autobobo.com";
            
            _httpContext.Response.Cookies.Add(cookie);


            #endregion

            #endregion


        }

        public virtual void SignOut()
        {
            //var CustomerID = GetAuthenticatedCustomerFromUserName(_httpContext.User.Identity.Name).Id;
            _cachedCustomer = null;


            try
            {
                #region 清除当前用户
                Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                if (hOnline[_httpContext.Session.SessionID] != null)
                {
                    hOnline.Remove(_httpContext.Session.SessionID);
                    _httpContext.Application.Lock();
                    _httpContext.Application["Online"] = hOnline;
                    _httpContext.Application.UnLock();
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
            finally
            {
                FormsAuthentication.SignOut();

                #region 退出登录时移除当前cookie的凭证
                //var currentSiteCookie = _httpContext.Request.Cookies["HC.AUTH"];// jia zs 0914 替换HC.AUTH为FormsAuthentication.FormsCookieName
                var currentSiteCookie = _httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];//jia 0914+
                if (currentSiteCookie != null)
                {
                    //currentSiteCookie.Domain = ".autobobo.com";////必须设置为这个域，不然response到了客户端 ，最主要的是  如果不设置Domain则不会覆盖本地存在的那个cookie 发布用
                    //if (FormsAuthentication.CookieDomain != null)
                    //{
                    //    currentSiteCookie.Domain = FormsAuthentication.CookieDomain;
                    //}
                    //currentSiteCookie.Expires = DateTime.Now.AddSeconds(-1);
                    //_httpContext.Response.Cookies.Add(currentSiteCookie);

                    #region 清除sso的cookie 不能通过这种方式去清除别个站点的cookie
                    //var cookieSSO = new HttpCookie("token");
                    //cookieSSO.Domain = ".autobobo.com";
                    //cookieSSO.Expires = DateTime.Now.AddSeconds(-1);
                    //_httpContext.Response.Cookies.Add(cookieSSO);
                    #endregion
                }
                #endregion

                #region

                /////0612 jakc  作用让它的request.IsAuthenticated为false
                System.Security.Principal.GenericIdentity gi = new System.Security.Principal.GenericIdentity("", "");
                string[] rolesi = { };
                System.Security.Principal.GenericPrincipal gpi = new System.Security.Principal.GenericPrincipal(gi, rolesi);
                _httpContext.User = gpi;
                /////0612 jakc  作用让它的request.IsAuthenticated为false
                #endregion
            }





        }


        /// <summary>
        /// 主要是供给WebWorkContext中使用  应为view中的config中指定了baseview是HC.Web.Framework.ViewEngines.Razor.WebViewPage 在其中做了调用当前用户
        /// </summary>
        /// <returns></returns>
        public virtual User_Info GetAuthenticatedCustomer()
        {
            //是否有本地票据
            if ((_httpContext.Request.IsAuthenticated) || ((_httpContext.User.Identity is FormsIdentity)))
            {
                if (equalSession_ID()) //判断是否为非法伪造cookie登录
                {
                    #region 如果本地验证通过(有本地票据)
                    if (_cachedCustomer != null)
                    {
                        #region 单用户登录处理
                        Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                        if (hOnline != null)
                        {
                            int i = 0;
                            while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
                            {
                                IDictionaryEnumerator idE = hOnline.GetEnumerator();

                                while (idE.MoveNext())
                                {
                                    if (idE.Value != null && idE.Value.ToString().Equals(_httpContext.User.Identity.Name))
                                    {
                                        //already login               
                                        if (idE.Key.ToString() != _httpContext.Session.SessionID)
                                        {
                                            _cachedCustomer = null;
                                        }

                                        break;
                                    }
                                }
                                i = i + 1;
                            }
                        }
                        #endregion
                        return _cachedCustomer;
                    }
                    ////////////////////////////////0612+//////////////////////////如果IsAuthenticated是true但_cachedCustomer被SignOut了，用户没有关浏览器(没有清楚本地的cookie)，
                    else if (!string.IsNullOrEmpty(_httpContext.User.Identity.Name))
                    {

                        #region 单用户处理

                        Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                        if (hOnline != null)
                        {
                            int i = 0;
                            while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
                            {
                                IDictionaryEnumerator idE = hOnline.GetEnumerator();

                                while (idE.MoveNext())
                                {
                                    if (idE.Value != null && idE.Value.ToString().Equals(_httpContext.User.Identity.Name))
                                    {
                                        //already login               
                                        if (idE.Key.ToString() != _httpContext.Session.SessionID)
                                        {
                                            return null;
                                        }
                                        break;
                                    }
                                }
                                i = i + 1;
                            }
                        }

                        #endregion

                        _cachedCustomer = GetAuthenticatedCustomerFromUserName(_httpContext.User.Identity.Name);
                        return _cachedCustomer;
                    }
                    ////////////////////////////////0612end+//////////////////////////
                    else
                    {
                        return null;
                    }
                    #endregion
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 主要是判断是否已登录 如果没有登录则跳转
        /// </summary>
        /// <param name="placeholder"></param>
        /// <returns></returns>
        public virtual object GetAuthenticatedCustomer(string placeholder)
        {
            //是否有本地票据
            if ((_httpContext.Request.IsAuthenticated) || ((_httpContext.User.Identity is FormsIdentity)))
            {
                #region 如果有本地票据
                if (equalSession_ID()) //判断是否为非法伪造cookie登录
                {

                    #region 如果本地验证通过(有本地票据)
                    if (_cachedCustomer != null)
                    {
                        #region 单用户登录处理
                        Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                        if (hOnline != null)
                        {
                            int i = 0;
                            while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
                            {
                                IDictionaryEnumerator idE = hOnline.GetEnumerator();

                                while (idE.MoveNext())
                                {
                                    if (idE.Value != null && idE.Value.ToString().Equals(_httpContext.User.Identity.Name))
                                    {
                                        //already login               
                                        if (idE.Key.ToString() != _httpContext.Session.SessionID)
                                        {
                                            _cachedCustomer = null;
                                        }

                                        break;
                                    }
                                }
                                i = i + 1;
                            }
                        }
                        #endregion
                        return _cachedCustomer;
                    }
                    ////////////////////////////////0612+//////////////////////////如果IsAuthenticated是true但_cachedCustomer被SignOut了，用户没有关浏览器(没有清楚本地的cookie)，
                    else if (!string.IsNullOrEmpty(_httpContext.User.Identity.Name))
                    {
                        #region 单用户登录处理
                        #region abanden
                        //for (int i = 0; i < _httpContext.Session.Count; i++)
                        //{

                        //    if (_httpContext.User.Identity.Name == _httpContext.Session[i].ToString() )
                        //    {
                        //        if (_httpContext.Session.Keys[i] != _httpContext.Session.SessionID)
                        //        {
                        //            return null;
                        //        }
                        //        break;
                        //    }
                        //}
                        #endregion

                        #region new

                        Hashtable hOnline = (Hashtable)_httpContext.Application["Online"];
                        if (hOnline != null)
                        {
                            int i = 0;
                            while (i < hOnline.Count) //因小BUG所以增加此判断，强制查询到底  
                            {
                                IDictionaryEnumerator idE = hOnline.GetEnumerator();

                                while (idE.MoveNext())
                                {
                                    if (idE.Value != null && idE.Value.ToString().Equals(_httpContext.User.Identity.Name))
                                    {
                                        //already login               
                                        if (idE.Key.ToString() != _httpContext.Session.SessionID)
                                        {
                                            return null;
                                        }
                                        break;
                                    }
                                }
                                i = i + 1;
                            }
                        }

                        #endregion
                        #endregion
                        _cachedCustomer = GetAuthenticatedCustomerFromUserName(_httpContext.User.Identity.Name);
                        return _cachedCustomer;
                    }
                    ////////////////////////////////0612end+//////////////////////////
                    else
                    {
                        return null;
                    }
                    #endregion
                }
                else
                {
                    return null;
                }
                #endregion
            }
            else
            {
                return null;
            }
        }

        public virtual User_Info GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var username = ticket.UserData;//存放user的userName
            if (String.IsNullOrWhiteSpace(username))
                return null;
            #region 构造searchModel
            Maticsoft.Model.User_Info_Search uModel = new Maticsoft.Model.User_Info_Search();
            uModel.trantype = "ByName";
            uModel.para = username;
            //uModel.UserName = username;
            #endregion
            var customer = _customerService.GetUserInfo(uModel);//byname
            return customer;
        }

        public virtual User_Info GetAuthenticatedCustomerFromUserID(int id)
        {

            #region 构造searchModel
            Maticsoft.Model.User_Info_Search uModel = new Maticsoft.Model.User_Info_Search();
            uModel.trantype = "ByID";
            uModel.para = id.ToString();
            #endregion
            return _customerService.GetUserInfo(uModel);//byid 
        }


        public virtual User_Info GetAuthenticatedCustomerFromUserName(string name)
        {
            #region 构造searchModel
            Maticsoft.Model.User_Info_Search uModel = new Maticsoft.Model.User_Info_Search();
            uModel.trantype = "ByName";
            uModel.para = name;
            #endregion
            return _customerService.GetUserInfo(uModel);//byname
        }

        public bool equalSession_ID()
        {
            var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            var ticket = formsIdentity.Ticket;
            string datas = ticket.UserData;
            var sessIDcookieStore = datas.Split(':')[1];
            return sessIDcookieStore == _httpContext.Session.SessionID ? true : false;
        }

        /// <summary>
        /// 验证是不是本线程发出的请求，防止劫持token和url进行非法登录
        /// </summary>
        /// <returns></returns>
        public bool equalInsPctionToSession_ID()
        {
            var inspection = _httpContext.Request.QueryString["inspection"];
            if (!string.IsNullOrEmpty(inspection))
            {
                return inspection == _httpContext.Session.SessionID;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户注册成功后将状态置为已登录状态，还未实现 此时进入其他网站也处登录状态  
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="pwd"></param>
        public void registerSing(string uName, string pwd)
        {
            if (true)
            {
                var customer = GetAuthenticatedCustomerFromUserName(uName);
                _cachedCustomer = customer;

                #region 生成本地票据
                var now = DateTime.UtcNow.ToLocalTime();

                #region 获取当前会话sessionID  并存入ticket中
                var currentSession_ID = _httpContext.Session.SessionID;
                #endregion


                #region 设置票据中的信息
                var ticket = new FormsAuthenticationTicket(
                    1 /*version*/,
                     customer.UserName,
                    now,
                    now.Add(_expirationTimeSpan),
                    false,//持久化
                    string.Format("{0}:{1}", customer.Id.ToString(), currentSession_ID),//防伪
                    FormsAuthentication.FormsCookiePath);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                #endregion

                #region 设置cookie过期时间
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = false;
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                cookie.Path = FormsAuthentication.FormsCookiePath;
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }
                //cookie.Domain = ".autobobo.com";//正式发布用
                _httpContext.Response.Cookies.Add(cookie);
                #endregion
                #endregion
            }
            else
            {
                throw new Exception("用户名或者密码错误");
            }
        }

        /// <summary>
        /// 跨站点调用
        /// </summary>
        /// <param name="linqp"></param>
        public void getAuth(string linqp)
        {
            #region 联系passPort验证PassGUID的值(颁发的还是伪造的)根据令牌去获取凭证 并生成当前网站票据
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(UntilMethod.appsetingValue("ssoAddress"));//http://localhost:21619
            HttpResponseMessage response = client.GetAsync("/api/Token/" + linqp.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;//存的是用户的id
                if (result != null)//通过passport认证
                {
                    #region json处理
                    var ss = JObject.Parse(result);
                    var userID = ss["result"].ToString();
                    #endregion

                    #region 如果该cookieGuid的值有效 则取返回来的result(当前用户的id)
                    var customer = GetAuthenticatedCustomerFromUserID(int.Parse(userID));
                    _cachedCustomer = customer;
                    #endregion

                    #region 生成本地票据
                    var now = DateTime.UtcNow.ToLocalTime();

                    #region 获取当前会话sessionID  并存入ticket中
                    var currentSession_ID = _httpContext.Session.SessionID;
                    #endregion


                    #region 设置票据中的信息
                    var ticket = new FormsAuthenticationTicket(
                        1 /*version*/,
                         customer.UserName,
                        now,
                        now.Add(_expirationTimeSpan),
                        false,//持久化
                        string.Format("{0}:{1}", customer.Id.ToString(), currentSession_ID),
                        //customer.Id.ToString(),
                        FormsAuthentication.FormsCookiePath);
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    #endregion

                    #region 设置cookie过期时间
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.HttpOnly = false;
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    if (FormsAuthentication.CookieDomain != null)
                    {
                        cookie.Domain = FormsAuthentication.CookieDomain;
                    }
                    //cookie.Domain = ".autobobo.com";
                    _httpContext.Response.Cookies.Add(cookie);
                    #endregion

                    #endregion
                }
            }
            #endregion
        }
    }
}
