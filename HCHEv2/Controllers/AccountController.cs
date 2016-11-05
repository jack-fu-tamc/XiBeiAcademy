using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Model;
using HCHEv2.Models.Login;
using HC.Service.Authentication;
using HC.Service.Account;
using HC.Core.CommonMethod;
using HC.Data.ViewModels;
using HC.Service.User;
using HC.Service.OpLog;
using HC.Data.Models;
using HC.Web.Framework.Security;
using HC.Core.Infrastructure;
using HC.Core;
using HC.Web.Framework;


namespace HCHEv2.Controllers
{
     [noMobleAttribute]
    public class AccountController : Controller
    {
        #region fild
        private IAuthenticationService _iauthenticationService;
        private IUser_InfoService _iuser_InfoService;
        private IUserService _IuserService;
        private IWorkContext _IworkContext;
        #endregion

        #region cto
        public AccountController(IAuthenticationService iauthenticationService, IUser_InfoService iuser_infoService, IUserService iuserService, IWorkContext _webWorKContext)
        {//
            this._iauthenticationService = iauthenticationService;
            this._iuser_InfoService = iuser_infoService;
            this._IuserService = iuserService;
            this._IworkContext = _webWorKContext;
        }
        #endregion

        #region commonMethod
        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [NonAction]
        private User_Info BoolSuccess(string uName, string pwd)
        {
            return _iuser_InfoService.authentiUser(uName, UntilMethod.Md5Encrypt(pwd));
        }


        private void addOpLog(string desc, string result, int userID, string userAccount, string userRealName)
        {
            var _opLogService = EngineContext.Current.Resolve<IopLogService>();

            var entity = new OpLog();
            entity.UserID = userID;
            entity.Id = System.Guid.NewGuid();
            entity.CreateTime = DateTime.Now;
            entity.OpDescriptions = desc;
            entity.OpResult = result;
            entity.UserAccount = userAccount;
            entity.UserRealName = userRealName;
            _opLogService.AddOpLog(entity);
        }


        //private void addIllegalUser(string uName, DateTime attemptTime, string ipAddress)
        //{

        //}

        #endregion

        /// <summary>
        /// 登录Get
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        //[OutputCache(Duration = 86400)]//发布时取消
       
        [AllowAnonymous]
        [HCHttpsRequirementAttribute(SslRequirement.Yes)]
        public ActionResult Login(string returnUrl)
        {
            var LoginTimes = TempData["LoginTimes"];
            if (LoginTimes == null)
            {
                TempData["LoginTimes"] = 1;
            }
            else
            {
                TempData["LoginTimes"] = LoginTimes;
                //var curentLoginTimes = int.Parse(TempData["LoginTimes"].ToString()) + 1;
                //TempData["LoginTimes"] = curentLoginTimes;
                //TempData["LoginTimes"] = int.Parse(TempData["LoginTimes"].ToString());
            }

            ViewBag.ReturnUrl = returnUrl;

            //var isNeedCatp = true;

            //try
            //{
            //     isNeedCatp = Convert.ToBoolean(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("verificationCode"));
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("xml错误");
            //}
            var isNeedCatp = Convert.ToBoolean(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("verificationCode"));
            ViewBag.isNeedCatp = isNeedCatp;
            return View();
        }



        /// <summary>
        /// 登录Post
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            //ModelState.Add
            var lockTimeLength = int.Parse(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("lcokTimeLenth"));
            var loginTimesMax = int.Parse(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("lockLogin"));
            var pwdProceeTime = int.Parse(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("overdueHoru"));
            var isNeedCatp = Convert.ToBoolean(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("verificationCode"));
            var isSSl = Convert.ToBoolean(HC.Core.CommonMethod.UntilMethod.getAppSettingValue("OpenSSl"));

            ViewBag.isNeedCatp = isNeedCatp;
            ViewBag.ReturnUrl = ReturnUrl;
            #region 登录次数相关
            //ViewData["LoginTimes"] = Request.Form["loginTimes"].ToString();
            //if (int.Parse(ViewData["LoginTimes"].ToString()) < 2)
            //{
            //    ViewData["LoginTimes"] = int.Parse(ViewData["LoginTimes"].ToString()) + 1;
            //}

            //var loginTimes = int.Parse(ViewData["LoginTimes"].ToString());//登录次数
            //ViewData["LoginTimes"] = int.Parse(ViewData["LoginTimes"].ToString()) + 1;
            //try
            //{

            //}
            int loginTimes = 1;
            try
            {
                loginTimes = int.Parse(TempData["LoginTimes"].ToString()) + 1;
            }
            catch (Exception ex)
            {
                loginTimes = 1;
            }
           
            TempData["LoginTimes"] = loginTimes;



            #region 检查该用户是否被锁定 以及用户名是否有效
            var logUser = _IuserService.GetUserByName(model.UserName);
            if (logUser != null && logUser.LockTime != null && (DateTime.Now - (DateTime)logUser.LockTime).TotalMinutes < lockTimeLength * 60)
            {
                addOpLog("登录", "失败,该用户被锁定", logUser.UserID, model.UserName, logUser.RealName);
                ModelState.AddModelError("loginFaile", "该用户已被锁定，请" + lockTimeLength + "小时后重试");
                return View(model);
            }
            //else if (logUser == null)//非法用户名尝试
            if (logUser == null)//非法用户名尝试
            {
                var ipAddress = Request.UserHostAddress ?? "";
                addOpLog("非法登录用户名不存在", "失败", 0, model.UserName, ipAddress);
                ModelState.AddModelError("loginFaile", "该用户已被锁定并记录");
                return View(model);


                //var _iopLogService = EngineContext.Current.Resolve<IopLogService>();
                //var attemptUser = _iopLogService.getByName(model.UserName);
                //if (attemptUser != null && (DateTime.Now - attemptUser.CreateTime).TotalMinutes < lockTimeLength * 60)
                //{
                //    ModelState.AddModelError("loginFaile", "该用户已被锁定，请" + lockTimeLength + "小时后重试");
                //    return View(model);
                //}
            }
            #endregion

            #region 验证码
            bool captValid = true;
            if (isNeedCatp)
            {
                if (Session["captchaText"] != null)
                    captValid = Session["captchaText"].ToString().ToLower() == model.CaptchaCode.ToLower();
                else
                    captValid = false;
            }
           
            //if (Session["captchaText"].ToString().ToLower() != model.CaptchaCode.ToLower())
            //{
            //    ModelState.AddModelError("loginFaile", "验证码错误！");
            //    return View(model);
            //}
            #endregion


            var loginResult = BoolSuccess(model.UserName, model.Password);
            if (loginResult != null && loginResult.effctive&&captValid)
            {

                #region 登录成功后要重置TempData["LoginTimes"]
                var releaseTemp = TempData["LoginTimes"];
                #endregion

                #region 密码更改周期处理
                if ((loginResult.passWordTime.AddDays(pwdProceeTime) - DateTime.Now).TotalDays <= 0&&pwdProceeTime>0)
                {
                    TempData["UserName"] = loginResult.UserName;
                    return Redirect("/manage/home/ChangePassWord/");
                }
                #endregion
               
                #region 登录
                _iauthenticationService.SignIn(loginResult);
                addOpLog("登录", "成功", loginResult.Id, loginResult.UserName, loginResult.RealName);
                #endregion

                if (string.IsNullOrEmpty(ReturnUrl))
                    return Redirect("/manage");
                else
                    return Redirect("/manage");
            }
            //登录用户名或密码错误  失效用户  验证码错误 时
            else
            {
                #region 日志记录
                //var logUser = _IuserService.GetUserByName(model.UserName);
                //if (logUser != null)
                //{
                //    if (!logUser.Effective)
                //        addOpLog("登录", "失败,该用户是无效用户", logUser.UserID, model.UserName, logUser.RealName);
                //    else
                //        addOpLog("登录", "失败", logUser.UserID, model.UserName, logUser.RealName);
                //}
                //else
                //{
                //    addOpLog("非法登录 用户名不存在", "失败", 0, model.UserName, "");
                //}
                #endregion

                if (loginResult != null)//
                {
                    if (!loginResult.effctive)//
                    {
                        addOpLog("登录", "失败,该用户已被禁用", loginResult.Id, model.UserName, loginResult.RealName);
                        ModelState.AddModelError("loginFaile", "该用户已被禁用!");
                    }

                    if (!captValid)
                    {
                        ModelState.AddModelError("loginFaile", "验证码错误！");
                    }
                }
                else
                {
                    //if (logUser == null)
                    //{
                    //    addOpLog("非法登录用户名不存在", "失败", 0, model.UserName, "");
                    //}
                    //else
                    //{
                    //    addOpLog("登录", "失败", logUser.UserID, model.UserName, "");
                    //}
                    addOpLog("登录", "失败", logUser.UserID, model.UserName, "");
                    ModelState.AddModelError("loginFaile", "用户名或密码错误！");

                    if (loginTimes >= loginTimesMax)
                    {
                        if (logUser != null)
                        {
                            logUser.LockTime = DateTime.Now;
                            _IuserService.UpdateUser(logUser);
                        }
                    } 
                }

                //if (!captValid)
                //{
                //    ModelState.AddModelError("loginFaile", "验证码错误！");
                //}

                //ModelState["CaptchaCode"].Errors.Clear();

                //if (loginTimes >= loginTimesMax)
                //{
                //    if (logUser != null)
                //    {
                //        logUser.LockTime = DateTime.Now;
                //        _IuserService.UpdateUser(logUser);
                //    }
                //}
               
                return View(model);
            }
            #endregion
        }



        //[AllowAnonymous]
        //public ActionResult register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (_IuserService.GetUserByName(model.Uname) == null)
        //        {
        //            var userEntity = new CxUser();
        //            userEntity.UserName = model.Uname;
        //            userEntity.UserPassword = UntilMethod.Md5Encrypt(model.Pwd);
        //            userEntity.isAdmin = false;
        //            _IuserService.AddUser(userEntity);

        //            //赋予已登录权限

        //            _iauthenticationService.SignIn(_iuser_InfoService.GetUserInfo(new User_Info_Search() { para = model.Uname, trantype = "ByName" }));

        //            //return RedirectToAction("Index", "UserCenter");
        //            return Redirect("/UserCenter/Index");
        //        }
        //        else
        //        {
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}


        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult isExsitOfUserName(string uName)
        //{
        //    var userEntity = _IuserService.GetUserByName(uName);
        //    if (userEntity != null)
        //    {
        //        return Json("Y", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("N", JsonRequestBehavior.AllowGet);
        //    }
        //}




        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOff()
        {
            if (_IworkContext.CurrentCustomer != null)
            {
                addOpLog("退出", "成功", _IworkContext.CurrentCustomer.Id, _IworkContext.CurrentCustomer.UserName, _IworkContext.CurrentCustomer.RealName);
            }
            _iauthenticationService.SignOut();
            //return RedirectToAction("login", "account");
            return Redirect("/");
        }
    }


}
