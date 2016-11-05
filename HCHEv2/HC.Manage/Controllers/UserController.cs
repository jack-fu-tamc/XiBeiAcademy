using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using HC.Service.User;
using HC.Service.Section;
using HC.Service.UserGroup;
using ResposityOfEf;
using HC.Data.Models;
using HC.Data.ViewModels;
using Telerik.Web.Mvc;
using HC.Core.CommonMethod;
using Hche.Common.Enum;
using System.Text.RegularExpressions;

namespace HC.Manage.Controllers
{
    public class UserController : ManageBaseController
    {
        #region field
        private IUserService _iuserService;
        private IuserGroupService _iuserGroupService;
        private ISectionService _isectionService;
        #endregion

        #region cto
        public UserController(IUserService iuserService,IuserGroupService iuserGroupService,ISectionService isectionService)
        {
            this._iuserService = iuserService;
            this._iuserGroupService = iuserGroupService;
            this._isectionService = isectionService;
        }
        #endregion

        #region common
        public HC.Data.Models.CxUser getUser(string uName)
        {
            return _iuserService.GetUserByName(uName);
        }
        #endregion

        /// <summary>
        /// 用户管理 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission; 
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();            
            #endregion


            #region 防伪编码
            var token = Guid.NewGuid().ToString();
            ViewBag.token = token;
            TempData["addToken"] = token;
            #endregion



            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
            }


            var model = new UserManageModel();
            ViewBag.curentUserID = curentUser.UserID;
            ViewBag.userGroup = _iuserGroupService.GetAllGroup();
            return View(model);
        }


        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult ajaxUser()
        {
            //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            //var curentUser = _iuserService.getUserByID(user.Id);

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
                //return new JsonResult() { Data = "", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            #endregion
            
            var AllUsers=_iuserService.GetAllUsers();
            //if (curentUser.UserGroupID == 1)
            //{

            //}
            //else
            //{
            //    AllUsers = AllUsers.Where(x => x.UserGroupID != 1).ToList();
            //}


            var model = new GridModel<CxUser>() { Data = AllUsers.Select(x => new CxUser() { Effective = x.Effective, RealName = x.RealName, UserName = x.UserName, UserPassword = "******", UserID=x.UserID, Address=x.UserGroup.GroupName }), Total = AllUsers.Count };
            //return Json(model, JsonRequestBehavior.AllowGet);
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult getUser(int id)
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
                //return new JsonResult() { Data = "", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            #endregion

           

            var result= _iuserService.getUserByID(id);
            return new JsonResult() { Data = new { UserID = result.UserID, UserName = result.UserName, RealName = result.RealName, UserGroupID = result.UserGroupID, Effective = result.Effective }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]       
        public ActionResult AddUser(CxUser model)
        {

           


            #region 访问权限
            var user1 = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user1.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
                //return Json(new { state = "Error", mes = "发生错误" });
            }
            #endregion


            #region 安全校验
            var unitiM = new UntilMethod();
            
            var serverToken=TempData["addToken"].ToString();
            var ClientToken = Request["addToken"] == null ? "" : Request["addToken"].ToString();
            if (unitiM.DecryptDES(ClientToken, "jack")!= serverToken)
            {
                TempData["addToken"] = serverToken;
                return new RedirectResult("/Home/deny");
            }
            #endregion

            #region 解密
            model.UserName = unitiM.DecryptDES(model.UserName, "jack");
            model.UserPassword = unitiM.DecryptDES(model.UserPassword, "jack");
            #endregion


            var length = int.Parse(UntilMethod.getAppSettingValue("passWordLength").ToString());

            #region 密码强度校验
            var regex = new Regex(@"(?=.*[0-9])(?=.*[a-zA-Z])(?=([\x21-\x7e]+)[^a-zA-Z0-9]).{" + length + ",15}", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            if (!regex.IsMatch(model.UserPassword))
            {
                return Json(new { state = "Error", mes = "密码格式不符，请输入" + length + "-15位，并含有数字字母和特殊字符" });
            }


            #endregion

            try
            {


                if (model.UserID > 0)//更新
                {
                    var user = _iuserService.getUserByID(model.UserID);
                    user.UserPassword = UntilMethod.Md5Encrypt(model.UserPassword);
                    user.RealName = model.RealName;
                    user.UserGroupID = model.UserGroupID;
                    user.passWordTime = DateTime.Now;
                    user.Effective = model.Effective;
                    _iuserService.UpdateUser(user);
                    AddOpLog("用户修改,修改账户:" + user.UserName.ToString() + " 角色:" + user.UserGroup.GroupName);
                }
                else//新增
                {

                    #region 用户名重复校验
                    var CheckUser = getUser(model.UserName);
                    if (CheckUser != null)
                    {
                        return Json(new { state = "Error", mes = "用户名已存在" });
                    }
                    #endregion

                    #region 获取密码过期时常
                    var overdueTimeLenth = -int.Parse(UntilMethod.getAppSettingValue("overdueHoru").ToString());
                    #endregion

                    model.isAdmin = true;
                    model.UserPassword = UntilMethod.Md5Encrypt(model.UserPassword);
                    //model.passWordTime = DateTime.Now;

                    model.passWordTime = DateTime.Now.AddDays(overdueTimeLenth);

                    _iuserService.AddUser(model);
                   var addUserGroup= _iuserGroupService.GetByID(model.UserGroupID);
                   AddOpLog("用户新增,新增账户:" + model.UserName.ToString() + " 角色:" + addUserGroup.GroupName);
                }
                return Json(new { state="OK",mes=""});
            }
            catch (Exception ex)
            {
                return Json(new { state = "Error", mes = "发生错误" });
            }
           
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult deleteUser(int id)
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion


            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
                //return Json("Error", JsonRequestBehavior.AllowGet);
            }


            var userentity=_iuserService.getUserByID(id);
            //var curentUserInfo = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            if (userentity!=null)
            {
                AddOpLog("删除账户:" + userentity.UserName.ToString());
                _iuserService.DeleteUser(userentity);               
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }      

        /// <summary>
        /// 用户角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Roles()
        {
            //var roles = _iuserGroupService.GetAllGroup().Where(x => x.GroupName != "管理员").ToList();
            //ViewBag.roles = roles;
            //var levelOneSection = _isectionService.getAllSection().SectionLists.Where(x => x.ParentID == 0).ToList();
            //ViewBag.levelOneSection = levelOneSection;

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion


            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
            }



            var model = new RolesManageModel();
            return View(model);
        }

        /// <summary>
        /// 角色异步数据
        /// </summary>
        /// <returns></returns>
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult ajaxRoles()
        {
            var roles = _iuserGroupService.GetAllGroup().ToList();
            var model = new GridModel<UserGroup>()
            {
                Data = roles.Select(x => new HC.Data.Models.UserGroup() { Comments=x.Comments, UserGroupID=x.UserGroupID, GroupName=x.GroupName, CxUsers=null, SectionPermission="", ManagePermission=""  }),
                Total = roles.Count()
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddUserGroup(int id=0)
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion


            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
            }




            var model = new UserGroup();
            if (id > 0)
            {
                model = _iuserGroupService.GetByID(id);
            }

            #region 一级栏目
            var NewsClass = _isectionService.getAllSection();
            var levelOne = NewsClass.SectionLists.Where(x => x.ParentID == 0);
            ViewBag.MultipleLevelOne = levelOne.ToList();
            #endregion

            return View(model);
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult _AddUserGroup(UserGroup model)
        {



            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return new RedirectResult("/Home/deny");
            }
            #endregion


           

            try
            {
                if (model.UserGroupID > 0)//更新
                {
                    var entity = _iuserGroupService.GetByID(model.UserGroupID);
                    entity.GroupName = model.GroupName;
                    entity.Comments = model.Comments;
                    entity.ManagePermission = model.ManagePermission;
                    entity.SectionPermission = model.SectionPermission;
                    _iuserGroupService.UpdateEntity(entity);
                    AddOpLog("修改角色:" + model.GroupName);
                }
                else//新增
                {
                   _iuserGroupService.AddEntity(model);
                   AddOpLog("新增角色:" + model.GroupName);
                }
                return Json("success");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }                     
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _DeleteUserGroup(int id)
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion


            if (!managePermission.Contains((int)SortEnum.sortClass.userManage))
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }



            var uGroup = _iuserGroupService.GetByID(id);
            if (uGroup.CxUsers.Count > 0)
            {
                return Json("该角色不能删除，有级联数据", JsonRequestBehavior.AllowGet);
            }
            else
            {
                AddOpLog("删除角色:" + uGroup.GroupName);
                _iuserGroupService.DeleteEntity(uGroup);
                return Json("success", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
