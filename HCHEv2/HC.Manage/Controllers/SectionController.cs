using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Service.Section;
using HC.Service.User;
using HC.Data.ViewModels;
using HC.Data.Models;
using HC.Service.Attachment;
using Newtonsoft.Json;
using Hche.Common.Enum;

namespace HC.Manage.Controllers
{
    public class SectionController : ManageBaseController
    {

        #region
        private ISectionService _isectionService;
        private IAttachmentService _iattachmentservice;
        private IUserService _iuserService;
        #endregion

        #region cto
        public SectionController(IUserService iuserService, ISectionService isectionService, IAttachmentService iAttachmentService)
        {
            this._iuserService = iuserService;
            this._isectionService = isectionService;
            this._iattachmentservice = iAttachmentService;
        }
        #endregion

        /// <summary>
        /// 栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult SectionManage()
        {
            try
            {

                #region 访问权限
                var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
                var curentUser = _iuserService.getUserByID(user.Id);
                var ManagePermission = curentUser.UserGroup.ManagePermission;
                List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
                var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

                if (curentUser.UserGroupID != 1)
                {
                    if (!managePermission.Contains((int)SortEnum.sortClass.newsManage))
                    {
                        return new RedirectResult("/Home/deny");
                    }
                }
                #endregion

                #region 栏目权限
                //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
                //var curentUser = _iuserService.getUserByID(user.Id);
                var sectionPermission = curentUser.UserGroup.SectionPermission;
                List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
                var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
                ViewBag.sectionIDs = sectionIDs;

                #endregion

                var model = _isectionService.getAllSection();
                return View(model);
            }
            catch
            {
                return Redirect("/");
            }
        }



        public ActionResult AddSection(int ParentID = 0, int id = 0)
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.newsManage))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion

            var pId = -1;
            var model = new NewsClass();
            if (id != 0)//编辑
            {
                model = _isectionService.getNewsClassByID(id);
                model.ClassName = HttpUtility.HtmlDecode(model.ClassName);
                model.linkAddress = HttpUtility.HtmlDecode(model.linkAddress);
                model.NaviContent = HttpUtility.HtmlDecode(model.NaviContent);
                model.PageContent = HttpUtility.HtmlDecode(model.PageContent);
                pId = model.ParentID;
            }


            var parentid = Request.QueryString["ParentID"] == null ? 0 : int.Parse(Request.QueryString["ParentID"]);
            if ((ParentID == 0 && id == 0) || pId == 0)
            {
                ViewBag.isLevelOne = true;//控制是否显示 缩略图 和 banner图
            }
            else
            {
                ViewBag.isLevelOne = false;
            }


            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSection(NewsClass model)
        {



            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.newsManage))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion







            if (model.ClassID != 0)//更新
            {
                var entity = _isectionService.getNewsClassByID(model.ClassID);
                entity.ClassName = HttpUtility.HtmlEncode(model.ClassName);
                entity.NaviPIC = model.NaviPIC;
                entity.NaviContent = HttpUtility.HtmlEncode(model.NaviContent);
                entity.ClassOrder = model.ClassOrder;
                entity.IsSingle = model.IsSingle;
                entity.IsShowInNav = model.IsShowInNav;
                entity.PageContent = HttpUtility.HtmlEncode(model.PageContent);
                entity.linkAddress = HttpUtility.HtmlEncode(model.linkAddress);
                entity.SmallPic = model.SmallPic;
                _isectionService.UpdataNewsClass(entity);


                #region 相册处理

                #region 删除相册
                var exsitPicsIDs = new List<int>();
                foreach (var pic in entity.Attachments.Where(x => x.AttaType == 1))
                {
                    exsitPicsIDs.Add(pic.AttachmentID);
                }
                foreach (var picid in exsitPicsIDs)
                {
                    _iattachmentservice.deleteAttachment(_iattachmentservice.getByID(picid));
                }
                #endregion

                #region 添加相册
                if (!string.IsNullOrEmpty(Request.Form["albumData"]))
                {
                    var albumLists = JsonConvert.DeserializeObject<attchments>(Request.Form["albumData"]);
                    if (albumLists.albumList.Count > 0)
                    {
                        foreach (var attch in albumLists.albumList)
                        {
                            _iattachmentservice.addAttachment(new Attachment() { AttaDescribe = attch.AttchmentIllustrate, AttaType = 1, AttaOrders = 0, AttaUrl = attch.AttachmentUrl, ClassID = model.ClassID });
                        }
                    }
                }
                #endregion

                #endregion

                AddOpLog("栏目修改,栏目ID:" + entity.ClassID.ToString());

            }
            else//新增
            {
                model.ClassName = HttpUtility.HtmlEncode(model.ClassName);
                model.NaviContent = HttpUtility.HtmlEncode(model.NaviContent);
                model.linkAddress = HttpUtility.HtmlEncode(model.linkAddress);
                model.PageContent = HttpUtility.HtmlEncode(model.PageContent);
                _isectionService.AddNewClass(model);
                var id = model.ClassID;

                #region 单页相册处理
                if (!string.IsNullOrEmpty(Request.Form["albumData"]))
                {
                    var albumLists = JsonConvert.DeserializeObject<attchments>(Request.Form["albumData"]);
                    if (albumLists.albumList.Count > 0)
                    {
                        foreach (var attch in albumLists.albumList)
                        {
                            _iattachmentservice.addAttachment(new Attachment() { AttaDescribe = attch.AttchmentIllustrate, AttaType = 1, AttaOrders = 0, AttaUrl = attch.AttachmentUrl, ClassID = model.ClassID });
                        }
                    }
                }
                #endregion

                AddOpLog("栏目新增,栏目ID:" + id.ToString());
            }
            return RedirectToAction("SectionManage");
        }


        public ActionResult DeleteSection(int id)
        {



            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.newsManage))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion



            #region 栏目权限
            //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            //var curentUser = _iuserService.getUserByID(user.Id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion

            if (!sectionIDs.Contains(id))
            {
                return new RedirectResult("/Home/deny");
            }
            else
            {
                var entity = _isectionService.getNewsClassByID(id);
                var subSection = _isectionService.GetSubNewClassByParentNewClass(entity.ClassID);
                if (entity.News.Count > 0 || subSection.Count > 0)
                {
                    ErrorNotification("可能该栏目下存在新闻或者子栏目");
                }
                else
                {
                    _isectionService.DeleteNewClass(entity);
                    AddOpLog("栏目删除,栏目ID:" + id.ToString());
                }
                return RedirectToAction("SectionManage");
            }

        }



        /// <summary>
        /// 根据父id获取子栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult getSectionByID(int id)
        {
            var newsclasss = _isectionService.GetSubNewClassByParentNewClass(id);


            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();


            var str = "";
            foreach (var nclass in newsclasss)
            {
                if (!sectionIDs.Contains(nclass.ClassID))
                {
                    continue;
                }
                if (nclass.IsSingle == 0)
                {
                    //str += "<a classid=\"" + nclass.ClassID + "\" class=\"showNews\">" + nclass.ClassName + "</a>";
                    str += "<option value=\"" + nclass.ClassID.ToString() + "\">" + nclass.ClassName + "</option>";
                }
            }
            return Content(str, "text/html");
        }




        /// <summary>
        /// 添加新闻页面 下拉框选项 异步数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult newsManageSectionMenue(int id)
        {
            //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));

            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            ViewBag.sectionIDs = sectionIDs;
            return View();
        }


    }
}
