using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using HC.Data.ViewModels;
using HC.Data.Models;
using HC.Service.News;
using HC.Service.Section;
using HC.Service.Attachment;
using HC.Service.User;
using HC.Service.SqlQuery;
using ResposityOfEf;
using Newtonsoft.Json;
using Hche.Common.Enum;
using HC.Web.Framework;


namespace HC.Manage.Controllers
{
    public class NewsController : ManageBaseController
    {
        //
        // GET: /News/
        private INewsService _inewsService;
        private IAttachmentService _iattachmentservice;
        private ISectionService _isectionService;
        private IUserService _iuserService;
        private IsqlQueryService _isqlQueryService;
        public NewsController(IsqlQueryService isqlQueryService, IUserService iuserService, INewsService inewsService, IAttachmentService iAttachmentService, ISectionService isectionService)
        {
            this._iuserService = iuserService;
            this._inewsService = inewsService;
            this._iattachmentservice = iAttachmentService;
            this._isectionService = isectionService;
            this._isqlQueryService = isqlQueryService;
        }


        public ActionResult NewsManage()
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




            var model = new NewsModelList();

            #region 栏目权限
            //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            //var curentUser = _iuserService.getUserByID(user.Id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            try
            {
                List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
                var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
                ViewBag.DefaultSectionID = sectionIDs.FirstOrDefault();
            }
            catch
            {
                ViewBag.DefaultSectionID = 0;
            }
            ViewBag.markID = user.Id;
            #endregion

            //var sections = _isectionService.getAllSection().SectionLists.Where(x => x.ParentID == 0).ToList();
            //ViewBag.sections = sections;

            return View(model);
        }

        /// <summary>
        /// 新闻管理异步
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult NewsAjax(GridCommand cmd, SerchNewsModel model)
        {
            #region 时间初始化
            DateTime startTime = new DateTime(1949, 1, 1, 0, 0, 0);
            DateTime endTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.startTime))
            {
                startTime = Convert.ToDateTime(model.startTime);
            }


            if (!string.IsNullOrEmpty(model.endTime))
            {
                endTime = Convert.ToDateTime(model.endTime).AddMinutes(1439);
            }
            #endregion

            List<int> sectionIDs = new List<int>();
            if (model.NewsClassID == 0)
            {
                var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
                var curentUser = _iuserService.getUserByID(user.Id);
                var sectionPermission = curentUser.UserGroup.SectionPermission;
                List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
                sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            }



            var queryData = _inewsService.GetAllNewsByClassID(model.NewsClassID, sectionIDs, cmd.PageSize, cmd.Page - 1, model.searchKey, startTime, endTime, model.searchType, model.sortType);
            var PageList = queryData.ToList();

            var result = new GridModel<NewsWithNotNewsClass>()
            {
                Data = PageList.Select(x => new NewsWithNotNewsClass() { Author = x.Author, ClassID = x.ClassID, ClickNum = x.ClickNum, contentCount = x.contentCount, CreatTime = x.CreatTime.ToString("yyyy-MM-dd HH:mm"), isDelete = x.isDelete, IsHot = x.IsHot, IsRec = x.IsRec, NewsContent = "", NewsID = x.NewsID, NewsOrder = x.NewsOrder, NewsTitle = x.NewsTitle, NewsTitleCount = x.NewsTitleCount, NewsType = x.NewsType ?? 0, PicURL = x.PicURL }),
                Total = queryData.TotalCount
            };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //var result=new GridModel<News>(){ Data=queryData}
        }

        /// <summary>
        /// 回收站异步
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult NewsRecycleAjax(GridCommand cmd, SerchNewsModel model)
        {


            #region 时间初始化
            DateTime startTime = new DateTime(1949, 1, 1, 0, 0, 0);
            DateTime endTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.startTime))
            {
                startTime = Convert.ToDateTime(model.startTime);
            }


            if (!string.IsNullOrEmpty(model.endTime))
            {
                endTime = Convert.ToDateTime(model.endTime).AddMinutes(1439);
            }
            #endregion

            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));

            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();

            var queryData = _inewsService.GetRecycleNews(sectionIDs, cmd.PageSize, cmd.Page, startTime, endTime);
            var PageList = queryData.ToList();
            var result = new GridModel<NewsWithNotNewsClass>()
            {
                Data = PageList.Select(x => new NewsWithNotNewsClass() { Author = x.Author, ClassID = x.ClassID, ClickNum = x.ClickNum, contentCount = x.contentCount, CreatTime = x.CreatTime.ToString("yyyy-MM-dd HH:mm"), isDelete = x.isDelete, IsHot = x.IsHot, IsRec = x.IsRec, NewsContent = "", NewsID = x.NewsID, NewsOrder = x.NewsOrder, NewsTitle = x.NewsTitle, NewsTitleCount = x.NewsTitleCount, NewsType = x.NewsType ?? 0, PicURL = x.PicURL }),
                Total = queryData.TotalCount
            };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 新闻审核
        /// </summary>
        /// <returns></returns>
        public ActionResult verifyNews()
        {
            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.newsCheck))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            
            #endregion

            var model = new NewsModelList();
            return View(model);
        }

        /// <summary>
        /// 新闻审核异步数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost,GridAction(EnableCustomBinding=true)]
        public ActionResult _verifyNewsAjax(GridCommand cmd)
        {
            var queryData = _inewsService.getNoCheckedNews(cmd.Page - 1, cmd.PageSize);
            var PageList = queryData.ToList();

            var result = new GridModel<NewsWithNotNewsClass>()
            {
                Data = PageList.Select(x => new NewsWithNotNewsClass() { Author = x.Author, ClassID = x.ClassID, ClickNum = x.ClickNum, contentCount = x.contentCount, CreatTime = x.CreatTime.ToString("yyyy-MM-dd HH:mm"), isDelete = x.isDelete, IsHot = x.IsHot, IsRec = x.IsRec, NewsContent = "", NewsID = x.NewsID, NewsOrder = x.NewsOrder, NewsTitle = x.NewsTitle, NewsTitleCount = x.NewsTitleCount, NewsType = x.NewsType ?? 0, PicURL = x.PicURL, className =x.NewsClass.ClassName}),
                Total = queryData.TotalCount
            };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }


        /// <summary>
        /// 新闻审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult _ajaxCheckNews(string ids)
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.newsCheck))
                {
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            
            #endregion



            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {
                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));
                    entity.isDelete = 0;
                    _inewsService.UpdateNews(entity);
                    AddOpLog("新闻审核,newsID:" + leaveIds[i].ToString());
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult AddNews(int ClassID = 0, int id = 0)
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






            



            var model = new News();
            model.ClassID = ClassID;
            model.CreatTime = DateTime.Now;
            if (id != 0)//编辑
            {
                model = _inewsService.GetNewsByID(id);
                model.NewsTitle = HttpUtility.HtmlDecode(model.NewsTitle);
                model.DeputyTitle = HttpUtility.HtmlDecode(model.DeputyTitle);
                model.Author = HttpUtility.HtmlDecode(model.Author);
                model.NewsContent = HttpUtility.HtmlDecode(model.NewsContent);//1027
            }
            return View(model);
        }

        /// <summary>
        ///  新闻添加和编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[operationRecord("新闻添加")]
        public ActionResult AddNews(NewsViewModel viewModel)
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





            #region 直发权限
            //var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            //var curentUser = _iuserService.getUserByID(user.Id);
            //var ManagePermission = curentUser.UserGroup.ManagePermission;
            //List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            //var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();
            var isNochekPermission = managePermission.Contains((int)SortEnum.sortClass.newsPromptly) ? 0 : 2;
            #endregion
           
            
            var viewModelSer = Newtonsoft.Json.JsonConvert.DeserializeObject<NewsViewModel>(Request.Form["stringfyData"]);
            var model = viewModelSer.model;

            if (model.NewsID != 0)//更新
            {
                var entity = _inewsService.GetNewsByID(model.NewsID);
                entity.NewsContent = HttpUtility.HtmlEncode(model.NewsContent);//1027
                entity.ClassID = model.ClassID;
                entity.IsHot = model.IsHot;
                entity.IsRec = model.IsRec;
                entity.PicURL = model.PicURL;
                entity.NewsType = model.NewsType;
                entity.Author = HttpUtility.HtmlEncode(model.Author);
                entity.NewsTitle = HttpUtility.HtmlEncode(model.NewsTitle);
                entity.ArtistLevel = model.ArtistLevel;
                entity.ArtistTypeID = model.ArtistTypeID;
                entity.NewsOrder = model.NewsOrder;
                entity.DeputyTitle = HttpUtility.HtmlEncode(model.DeputyTitle);
                entity.CreatTime = Convert.ToDateTime(model.CreatTime);
                //entity.isDelete = isNochekPermission;
                _inewsService.UpdateNews(entity);

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
                if (viewModelSer.attchments.albumList.Count > 0)
                {
                    foreach (var attch in viewModelSer.attchments.albumList)
                    {
                        _iattachmentservice.addAttachment(new Attachment() { AttaDescribe =HttpUtility.HtmlEncode(attch.AttchmentIllustrate),AttaType = 1, AttaOrders = 1, AttaUrl = attch.AttachmentUrl, NewsID = model.NewsID });
                    }
                }
                #endregion

                #region 视频处理
                var vidoes = entity.Attachments.Where(x => x.AttaType == 2).FirstOrDefault();
                if (vidoes != null)
                {
                    _iattachmentservice.deleteAttachment(vidoes);
                }

                if (viewModelSer.viodeInfo.voidpicurl != "" && viewModelSer.viodeInfo.voidURL != "")
                {
                    _iattachmentservice.addAttachment(new Attachment() { AttaType = 2, AttaOrders = 0, AttaUrl = viewModelSer.viodeInfo.voidURL, AttaDescribe = viewModelSer.viodeInfo.voidpicurl, NewsID = model.NewsID, OriginalName = viewModelSer.viodeInfo.viodeOriginalName + "," + viewModelSer.viodeInfo.voidpicOriginalName });
                }

                #endregion

                
                AddOpLog("新闻编辑,newsID:"+model.NewsID.ToString());

            }
            else
            {
                //model.CreatTime = DateTime.Now;
                model.NewsContent = HttpUtility.HtmlEncode(model.NewsContent);//1027
                model.NewsTitle = HttpUtility.HtmlEncode(model.NewsTitle);
                model.DeputyTitle = HttpUtility.HtmlEncode(model.DeputyTitle);
                model.Author = HttpUtility.HtmlEncode(model.Author);
                model.isDelete = isNochekPermission;
                _inewsService.AddNews(model);

                #region 添加相册和视频
                if (viewModelSer.attchments.albumList.Count > 0)
                {
                    foreach (var attch in viewModelSer.attchments.albumList)
                    {
                        _iattachmentservice.addAttachment(new Attachment() { AttaDescribe = HttpUtility.HtmlEncode(attch.AttchmentIllustrate), AttaType = 1, AttaOrders = 0, AttaUrl = attch.AttachmentUrl, NewsID = model.NewsID });
                    }
                }
                if (viewModelSer.viodeInfo.voidpicurl != "" && viewModelSer.viodeInfo.voidURL != "")
                {
                    _iattachmentservice.addAttachment(new Attachment() { AttaType = 2, AttaOrders = 0, AttaUrl = viewModelSer.viodeInfo.voidURL, AttaDescribe = viewModelSer.viodeInfo.voidpicurl, NewsID = model.NewsID, OriginalName = viewModelSer.viodeInfo.viodeOriginalName + "," + viewModelSer.viodeInfo.voidpicOriginalName });
                }
                #endregion


                AddOpLog("新闻添加,newsID:" + model.NewsID.ToString());
            }
            //return RedirectToAction("NewsManage");
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult _ajaxNews(string ids)
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
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            #endregion

            #region 栏目权限
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion

            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {
                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));
                    if (!sectionIDs.Contains(entity.NewsClass.ClassID))//防止删除自己管理栏目之外的新闻
                    {
                        return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    entity.isDelete = 1;
                    _inewsService.UpdateNews(entity);
                    AddOpLog("新闻删除,newsID:" + leaveIds[i].ToString());
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

           
        }

        /// <summary>
        /// 新闻转移
        /// </summary>
        /// <param name="ids">新闻id</param>
        /// <param name="targetIDs">目标栏目id</param>
        /// <returns></returns>

        public ActionResult _transferNews(string ids)
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
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            #endregion


            #region 栏目权限
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion



            try
            {
                var targetIds = Request.QueryString["targetIDs"].ToString().Split(',');
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {                    
                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));
                    #region 新闻源栏目权限验证
                    if (!sectionIDs.Contains(entity.ClassID))
                    {
                        return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    #endregion
                    for (var j = 0; j < targetIds.Length; j++)
                    {
                        #region 目标栏目权限验证
                        if (!sectionIDs.Contains(int.Parse(targetIds[j])))
                        {
                            return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        #endregion

                        entity.ClassID = int.Parse(targetIds[j]);
                        _inewsService.UpdateNews(entity);
                    }
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 新闻复制
        /// </summary>
        /// <param name="ids">新闻id</param>
        /// <param name="targetIDs">目标栏目id</param>
        /// <returns></returns>
        public ActionResult _copyNews(string ids)
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
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            #endregion


            #region 栏目权限
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion


           


            try
            {
                var targetIds = Request.QueryString["targetIDs"].ToString().Split(',');
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {

                   


                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));


                    #region 新闻源栏目权限验证
                    if (!sectionIDs.Contains(entity.ClassID))
                    {
                        return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    #endregion

                    for (var j = 0; j < targetIds.Length; j++)
                    {

                        #region 目标栏目权限验证
                        if (!sectionIDs.Contains(int.Parse(targetIds[j])))
                        {
                            return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        #endregion


                        var newEntity = new HC.Data.Models.News();
                        #region 新闻构造
                        newEntity.Author = entity.Author;
                        newEntity.ClickNum = entity.ClickNum;
                        newEntity.CreatTime = entity.CreatTime;
                        newEntity.DeputyTitle = entity.DeputyTitle;
                        newEntity.isDelete = entity.isDelete;
                        newEntity.NewsContent = entity.NewsContent;
                        newEntity.NewsOrder = entity.NewsOrder;
                        newEntity.NewsTitle = entity.NewsTitle;
                        newEntity.PicURL = entity.PicURL;
                        newEntity.ClassID = int.Parse(targetIds[j]);
                        #endregion
                        _inewsService.AddNews(newEntity);

                        #region 附件处理
                        var newAttchment = entity.Attachments;
                        foreach (var att in newAttchment)
                        {
                            var newAtt = new HC.Data.Models.Attachment();
                            newAtt.NewsID = newEntity.NewsID;
                            newAtt.AttaType = att.AttaType;
                            newAtt.AttaUrl = att.AttaUrl;
                            newAtt.AttaOrders = att.AttaOrders;
                            newAtt.ClassID = att.ClassID;
                            newAtt.OriginalName = att.OriginalName;
                            newAtt.AttaDescribe = att.AttaDescribe;
                            _iattachmentservice.addAttachment(newAtt);
                        }

                        #endregion

                    }
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 获取新闻属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult _getNewsProperty(int id)
        {
            var entity = _inewsService.GetNewsByID(id);
            var result = new { isHot = entity.IsHot == 1 ? true : false, isRec = entity.IsRec == 1 ? true : false, Order = entity.NewsOrder };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 设置新闻属性
        /// </summary>
        /// <returns></returns>
        public ActionResult _setNewsProperty(int id)
        {
            try
            {
                var entity = _inewsService.GetNewsByID(id);
                var isHot = Request["isHot"].ToString() == "true" ? 1 : 0;
                var isRec = Request["isRec"].ToString() == "true" ? 1 : 0;
                entity.IsRec = isRec;
                entity.IsHot = isHot;
                entity.NewsOrder = int.Parse(Request["Order"].ToString());
                _inewsService.UpdateNews(entity);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 彻底删除新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult _deleteNewsAbsoulte(string ids)
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.recycle))
                {
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            #endregion

            #region 栏目权限
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));
            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            #endregion

            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {
                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));
                    if (!sectionIDs.Contains(entity.NewsClass.ClassID))//防止删除自己管理栏目之外的新闻
                    {
                        return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    var attaEntity = entity.Attachments.FirstOrDefault();
                    if (attaEntity != null)
                        _iattachmentservice.deleteAttachment(attaEntity);
                    _inewsService.DeleteNews(entity);
                    AddOpLog("新闻彻底删除,newsID:" + leaveIds[i].ToString());
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 恢复新闻
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult _recoverNews(string ids)
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.recycle))
                {
                    return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            #endregion

            try
            {
                var leaveIds = ids.Split(',');
                for (var i = 0; i < leaveIds.Length; i++)
                {

                    var entity = _inewsService.GetNewsByID(int.Parse(leaveIds[i]));
                    entity.isDelete = 2;
                    _inewsService.UpdateNews(entity);
                }
                return new JsonResult() { Data = "success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = "error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// 回收站
        /// </summary>
        /// <returns></returns>
        public ActionResult recycle()
        {

            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.recycle))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion

            var model = new NewsModelList();
            return View(model);
        }


        /// <summary>
        /// 当年每月新闻发布数量
        /// </summary>
        /// <returns></returns>
        public ActionResult analysisNews()
        {


            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.analysis))
                {
                    return new RedirectResult("/Home/deny");
                }
            }
            #endregion


            var reResult = _isqlQueryService.getNewsAnalsysMonth(DateTime.Now.Year.ToString()).ToList();
            var model = new List<string>();
            for (var i = 1; i <= 12; i++)
            {
               var filterRe= reResult.Where(x => x.monthStr == i).FirstOrDefault();
               if (filterRe != null)
               {
                   model.Add(filterRe.total.ToString());
               }
               else
               {
                   model.Add("0");
               }

            }
            ViewBag.model = string.Join(",", model);
            return View();
        }

        /// <summary>
        /// 新闻统计异步数据
        /// </summary>
        /// <returns></returns>
        public ActionResult _analysisNews()
        {



            #region 访问权限
            var user = (Maticsoft.Model.User_Info)ViewBag.curentUser;
            var curentUser = _iuserService.getUserByID(user.Id);
            var ManagePermission = curentUser.UserGroup.ManagePermission;
            List<string> managePermissionstr = new List<string>(ManagePermission.Split(','));
            var managePermission = managePermissionstr.Select(x => Convert.ToInt32(x)).ToList();

            if (curentUser.UserGroupID != 1)
            {
                if (!managePermission.Contains((int)SortEnum.sortClass.analysis))
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            #endregion


            var year = Request.QueryString["year"].ToString();

            var reResult = _isqlQueryService.getNewsAnalsysMonth(year).ToList();
            var model = new List<string>();
            for (var i = 1; i <= 12; i++)
            {
                var filterRe = reResult.Where(x => x.monthStr == i).FirstOrDefault();
                if (filterRe != null)
                {
                    model.Add(filterRe.total.ToString());
                }
                else
                {
                    model.Add("0");
                }

            }
            return Json(string.Join(",", model), JsonRequestBehavior.AllowGet);
        }

    }
}
