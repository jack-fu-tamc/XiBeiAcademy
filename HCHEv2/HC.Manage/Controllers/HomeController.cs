using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Data.Models;
using Telerik.Web.Mvc;
using HC.Data.ViewModels;
using HC.Service.ArtistType;
using HC.Service.SqlQuery;
using Newtonsoft.Json;
using ResposityOfEf;
using HC.Core.Infrastructure;
using HC.Service.OpLog;
using HC.Service.User;
using Hche.Common.Enum;
using HC.Core.CommonMethod;
using HC.Web.Framework;
using System.Text.RegularExpressions;

namespace HC.Manage.Controllers
{
    public class HomeController : ManageBaseController
    {
        #region fields
        private IartiseTypeService _iartistTypeService;
        private IsqlQueryService _isqlQueryService;
        private IUserService _iuserService;
        #endregion

        public HomeController(IUserService iuserService, IartiseTypeService iartistTypeService, IsqlQueryService isqlQueryService)
        {
            this._iartistTypeService = iartistTypeService;
            this._isqlQueryService = isqlQueryService;
            this._iuserService = iuserService;
        }


        public ActionResult Index()
        {

            #region 后台首页新闻审核显示
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;

            ViewBag.checkPermission = false;
            if (ManagePermission != null)
            {
                List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
                var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
                if (managePermission.Contains((int)SortEnum.sortClass.newsCheck) || (curentUser.UserGroupID == 1))
                {
                    ViewBag.checkPermission = true;
                }
            }
            

            #endregion


            ViewBag.newsTotalCount = _isqlQueryService.GetNewsCount().FirstOrDefault();
            ViewBag.notCheckMessage = _isqlQueryService.GetNotCheckedMessage().FirstOrDefault();
            ViewBag.newsMonthCount = _isqlQueryService.GetMonthNewsCount().FirstOrDefault();
            var clickNum = _isqlQueryService.GetTotalClick();
            ViewBag.totalClick = clickNum == null ? 0: clickNum.FirstOrDefault();
            return View();
        }


        /// <summary>
        /// 人员分类管理
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult PeopleCateManage()
        //{
        //    var model = new ArtistTypeListModel();
        //    return View(model);
        //}

        /// <summary>
        /// 戏种分类管理
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult OperaManage()
        //{
        //    var model = new ArtistTypeListModel();
        //    return View(model);
        //}



        /// <summary>
        /// 戏种分类异步数据获取
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult _ajaxOperaCateManage(GridCommand cmd)
        {
            var queryData = _iartistTypeService.GetAllArtisetOperaType();
            var result = new GridModel<ArtistType>()
            {
                Data = queryData.ToList().Select(x => new ArtistType() { ArtistCategory = x.ArtistCategory, ArtistTypeID = x.ArtistTypeID, Effective = x.Effective, TypeName = x.TypeName, News = null }),
                Total = queryData.ToList().Count
            };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// 人员分类异步数据获取
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult _ajaxPeopleCateManage(GridCommand cmd)
        {
            var queryData = _iartistTypeService.GetAllArtisetPeopleType();
            var result = new GridModel<ArtistType>()
            {
                Data = queryData.ToList().Select(x => new ArtistType() { ArtistCategory = x.ArtistCategory, ArtistTypeID = x.ArtistTypeID, Effective = x.Effective, TypeName = x.TypeName, News = null }),
                Total = queryData.ToList().Count
            };

            //return Content(JsonConvert.SerializeObject(result), "application/json ");

            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ajaxDeleteAtirstType(int id)
        {
            try
            {
                var artistTypeEntity = _iartistTypeService.getByID(id);
                _iartistTypeService.deleteArtistType(artistTypeEntity);
                return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 添加或 修改分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult _ajaxAddAtirstType()
        {
            var artistID = int.Parse(Request.QueryString["artistID"].ToString());
            var artistType = int.Parse(Request.QueryString["artistType"].ToString());
            var artistName = Request.QueryString["artistName"].ToString();
            try
            {
                if (artistID > -1)// Updata
                {
                    var artistEntity = _iartistTypeService.getByID(artistID);
                    artistEntity.TypeName = artistName;
                    _iartistTypeService.updateArtiseType(artistEntity);
                }
                else//Add
                {
                    var artistEntity = new ArtistType() { ArtistCategory = artistType, Effective = true, TypeName = artistName };
                    _iartistTypeService.addArtiseType(artistEntity);
                }
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        ///获取三级分类(人员或戏种) 新闻管理页面使用
        /// </summary>
        /// <returns></returns>
        public ActionResult getLevelThreeSection()
        {
            var artistType = int.Parse(Request.QueryString["artistType"].ToString());
            var artistsResult = new List<ArtistType>();
            if (artistType == 1)
            {
                artistsResult = _iartistTypeService.GetAllArtisetPeopleType().ToList();
            }
            else
            {
                artistsResult = _iartistTypeService.GetAllArtisetOperaType().ToList();
            }
            var str = "";
            foreach (var arts in artistsResult)
            {
                str += "<a classid=\"" + arts.ArtistTypeID + "\" class=\"showNews\">" + arts.TypeName + "</a>";
            }
            return Content(str, "text/html");
        }



        /// <summary>
        /// 日志记录
        /// </summary>
        /// <returns></returns>
        public ActionResult logRecord()
        {
            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.logRecord))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion

            var model = new LogResultViewModel();
            var users = _iuserService.GetAllUsers();
            ViewBag.users = users;

            return View(model);
        }


        /// <summary>
        /// 日志异步数据
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult _ajaxLogRecord(GridCommand cmd, SerchLogModel searchModel)
        {

            var _opLogService = EngineContext.Current.Resolve<IopLogService>();


            #region 时间初始化
            DateTime startTime = new DateTime(1949, 1, 1, 0, 0, 0);
            DateTime endTime = DateTime.Now;
            if (!string.IsNullOrEmpty(searchModel.startTime))
            {
                startTime = Convert.ToDateTime(searchModel.startTime);
            }


            if (!string.IsNullOrEmpty(searchModel.endTime))
            {
                endTime = Convert.ToDateTime(searchModel.endTime).AddMinutes(1439);
            }
            #endregion



            var queryData = _opLogService.GetAllLogs(cmd.PageSize, cmd.Page - 1, searchModel.UserID, startTime, endTime);
            var result = new GridModel<OpLog>()
            {
                Data = queryData.ToList(),
                Total = queryData.TotalCount
            };

            //return Content(JsonConvert.SerializeObject(result), "application/json ");
            return Json(result, JsonRequestBehavior.AllowGet);
            //return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// 站点设置
        /// </summary>
        /// <returns></returns>
        public ActionResult webSiteSet()
        {
            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.webSet))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion


            //UntilMethod.getAppSettingValue("indexPicAddress");

            var model = new WebSiteViewModel();
            model.lockLogin = int.Parse(UntilMethod.getAppSettingValue("lockLogin"));
            model.OpenSSl = Convert.ToBoolean(UntilMethod.getAppSettingValue("OpenSSl"));
            model.overdueHoru = int.Parse(UntilMethod.getAppSettingValue("overdueHoru"));
            model.passWordLength = int.Parse(UntilMethod.getAppSettingValue("passWordLength"));
            model.verificationCode = Convert.ToBoolean(UntilMethod.getAppSettingValue("verificationCode"));
            model.lcokTimeLenth = int.Parse(UntilMethod.getAppSettingValue("lcokTimeLenth"));
            return View(model);
        }

        /// <summary>
        /// 站点设置更改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _ChangeWebSite(WebSiteViewModel model)
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.webSet))
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            #endregion


            try
            {
                UntilMethod.writeAppSettingValue("lockLogin", model.lockLogin.ToString());
                UntilMethod.writeAppSettingValue("OpenSSl", model.OpenSSl.ToString());
                UntilMethod.writeAppSettingValue("overdueHoru", model.overdueHoru.ToString());
                UntilMethod.writeAppSettingValue("passWordLength", model.passWordLength.ToString());
                UntilMethod.writeAppSettingValue("verificationCode", model.verificationCode.ToString());
                UntilMethod.writeAppSettingValue("lcokTimeLenth", model.lcokTimeLenth.ToString());
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }



        [imgFileter]
        public ActionResult ChangePassWord()
        {
            try
            {
                //ViewBag.UserID = TempData["UserName"].ToString();
                var uName = TempData["UserName"].ToString();
                var udictionary = new Dictionary<string, string>();
                var token = Guid.NewGuid().ToString();
                ViewBag.Token = token;
                udictionary.Add(token, uName);
                TempData["uNameToken"] = udictionary;
                return View();
            }
            catch
            {
                return Redirect("/manage");
            }

        }

        [imgFileter]
        [HttpPost]
        public ActionResult _ChangePassword()
        {
            var length = int.Parse(UntilMethod.getAppSettingValue("passWordLength").ToString());
            try
            {

                var oldPwd = Request["oldPwd"].ToString().Trim();
                var newPwd = Request["NewPwd"].ToString().Trim();
                var udictionary = TempData["uNameToken"] as Dictionary<string, string>;
                var uID = "";

                #region 验证新密码规则
                var regex = new Regex(@"(?=.*[0-9])(?=.*[a-zA-Z])(?=([\x21-\x7e]+)[^a-zA-Z0-9]).{" + length + ",15}", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                if (regex.IsMatch(newPwd))
                {
                    #region 数据安全性校验
                    var utoken = Request["token"].ToString();
                    if (!udictionary.ContainsKey(utoken))
                    {
                        return Json(new { state = "error", mes = "非法操作!" });
                    }
                    #endregion

                    uID = udictionary[utoken];
                    var curentUser = _iuserService.GetUserByName(uID);

                    #region 原密码验证
                    if (curentUser.UserPassword == UntilMethod.Md5Encrypt(oldPwd))
                    {
                        curentUser.UserPassword = UntilMethod.Md5Encrypt(newPwd);
                        curentUser.passWordTime = DateTime.Now;
                        _iuserService.UpdateUser(curentUser);
                        return Json(new { state = "success" });
                    }
                    else
                    {
                        TempData["uNameToken"] = udictionary;
                        return Json(new { state = "error", mes = "与原密码不匹配" });
                    }
                    #endregion
                }
                else
                {
                    TempData["uNameToken"] = udictionary;
                    return Json(new { state = "error", mes = "密码格式不符，请输入" + length + "-15位，并含有数字字母和特殊字符" });
                }
                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { state = "error", mes = "发生错误" });
            }
        }


        /// <summary>
        /// 用户更改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetPassWord()
        {
            var token = Guid.NewGuid().ToString();
            ViewBag.Token = token;
            TempData["uToken"] = token;
            return View();
        }


        
        [HttpPost]
        public ActionResult _SetPassWord()
        {
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);


            var length = int.Parse(UntilMethod.getAppSettingValue("passWordLength").ToString());
            try
            {

                var oldPwd = Request["oldPwd"].ToString().Trim();
                var newPwd = Request["NewPwd"].ToString().Trim();
                var udictionary = TempData["uToken"];
               

                #region 验证新密码规则
                var regex = new Regex(@"(?=.*[0-9])(?=.*[a-zA-Z])(?=([\x21-\x7e]+)[^a-zA-Z0-9]).{" + length + ",15}", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                if (regex.IsMatch(newPwd))
                {
                    #region 数据安全性校验
                    var utoken = Request["token"].ToString();
                    if (udictionary.ToString()!=utoken)
                    {
                        return Json(new { state = "error", mes = "非法操作!" });
                    }
                    #endregion

                    

                    #region 原密码验证
                    if (curentUser.UserPassword == UntilMethod.Md5Encrypt(oldPwd))
                    {
                        curentUser.UserPassword = UntilMethod.Md5Encrypt(newPwd);
                        curentUser.passWordTime = DateTime.Now;
                        _iuserService.UpdateUser(curentUser);
                        return Json(new { state = "success" });
                    }
                    else
                    {
                        return Json(new { state = "error", mes = "与原密码不匹配" });
                    }
                    #endregion
                }
                else
                {
                    return Json(new { state = "error", mes = "密码格式不符，请输入" + length + "-15位，并含有数字字母和特殊字符" });
                }
                #endregion
            }
            catch (Exception ex)
            {
                return Json(new { state = "error", mes = "发生错误" });
            }
        }
    }
}