using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using HC.Data.Models;
using HC.Data.ViewModels;
using HC.Service.News;
using HC.Service.Section;
using HC.Service.LeaveMessage;
using HC.Service.ArtistType;
using HC.Service.SqlQuery;
using HC.Core;
using HC.Core.Infrastructure;
using ResposityOfEf;


namespace HCHEv2.Controllers
{
    public class InformationController : Controller
    {
        #region field
        private ISectionService _isectionService;
        private INewsService _inewsSevice;
        private IartiseTypeService _iartistTypeService;
        private ILeaveMessageService _ileaveMessageService;
        private IsqlQueryService _isqlQueryService;
        #endregion

        #region cto
        public InformationController(ISectionService isectionService, INewsService inewsService, ILeaveMessageService ileaveMessageService, IartiseTypeService iartistTypeService,IsqlQueryService isqlQueryService)
        {
            this._inewsSevice = inewsService;
            this._isectionService = isectionService;
            this._iartistTypeService = iartistTypeService;
            this._isqlQueryService = isqlQueryService;

            this._ileaveMessageService = ileaveMessageService;
        }
        #endregion



        #region common
        protected News getNextOrPrevNewsID(int classID, int NewsID, int getType)
        {           
            var ReNewsID= _isqlQueryService.GetNextOrPreNewsID(classID, NewsID, getType).FirstOrDefault();
            if (ReNewsID > -1)
            {
               return _inewsSevice.GetNewsByID(ReNewsID);
            }
            else
            {
                return null;
            }
        }


        #endregion


        #region 公共部分视图

        /// <summary>
        /// 公共左侧戏曲时迅
        /// </summary>
        /// <returns></returns>
        public ActionResult leftNewsDelivery()
        {
            var exceptClassIds = new List<int>();
            exceptClassIds.Add(2081);
            var model = _inewsSevice.GetLeftDeliveryNews(4, exceptClassIds).ToList();
            
            //model = AllNews.OrderByDescending(x => x.NewsID).Take(4).ToList();
            return View(model);
        }





        /// <summary>
        /// 左侧公共栏目导航
        /// </summary>
        /// <param name="id">当前栏目的父栏目的ID</param>
        /// <returns></returns>
        public ActionResult LeftNav(int id, string viewName = "default")
        {
            var curentClassId = int.Parse(ControllerContext.RouteData.Values["curenId"].ToString());
            ViewBag.curentClassId = curentClassId;
            ViewBag.toView = viewName;
            if (id != 0)
            {
                var model = _isectionService.GetSiblingNewsClass(id).Where(x => x.IsShowInNav == 1).OrderBy(x => x.ClassOrder).ToList();
                return View(model);
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 招生左侧菜单导航
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult LeftNavEnrol(int id, string viewName = "Enrol")
        {
            var curentClassId = int.Parse(ControllerContext.RouteData.Values["curenId"].ToString());
            ViewBag.curentClassId = curentClassId;
            ViewBag.toView = viewName;
            if (id != 0)
            {
                var model = _isectionService.GetSiblingNewsClass(id).Where(x => x.IsShowInNav == 1).OrderBy(x => x.ClassOrder).ToList();
                return View(model);
            }
            else
            {
                return null;
            }
        }


        #endregion

        #region 视图获取Model

        /// <summary>
        /// 文字列表Model 有热点
        /// </summary>
        /// <param name="page"></param>
        /// <param name="collectionNews"></param>
        /// <param name="hotNews"></param>
        /// <returns></returns>
        public List<News> GetNormalModel(int page, ICollection<News> collectionNews, News hotNews)
        {
            #region 列表第一页热点图文新闻
            if (page == 1)//第一页显示热点图文
            {
                hotNews = collectionNews.Where(x => x.IsHot == 1).OrderByDescending(x => x.NewsOrder).ThenByDescending(x => x.CreatTime).FirstOrDefault();// .OrderByDescending(x => x.CreatTime)
                ViewData["hotNews"] = hotNews;
            }
            #endregion
            if (hotNews != null)
                return collectionNews.Where(x => x.NewsID != hotNews.NewsID).OrderByDescending(x => x.NewsOrder).ThenByDescending(x=>x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
            else
                return collectionNews.OrderByDescending(x => x.NewsOrder).ThenByDescending(x=>x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
        }


        public List<News> GetModel(int page, ICollection<News> collectionNews,int pagesize)
        {
            return collectionNews.OrderByDescending(x => x.NewsOrder).ThenByDescending(x => x.CreatTime).Skip((page - 1) * pagesize).Take(pagesize).ToList();
            //return collectionNews.OrderByDescending(x => x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
        }


        /// <summary>
        /// 各团优秀剧目||老艺术家 三级
        /// </summary>
        /// <param name="page"></param>
        /// <param name="collectionNews"></param>
        /// <param name="artistTypeId"></param>
        /// <returns></returns>
        public List<News> GetThreelevelModel(int page, ICollection<News> collectionNews, int artistTypeId, out int totalCount)
        {
            totalCount = collectionNews.Where(x => x.ArtistTypeID == artistTypeId).Count();
            return collectionNews.Where(x => x.ArtistTypeID == artistTypeId).OrderByDescending(x => x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
        }


        public List<News> GetSpecialModel(int page, ICollection<News> collectionNews, int artistTypeId, out int total, int artistLevel)
        {
            total = collectionNews.Where(x => x.ArtistTypeID == artistTypeId && x.ArtistLevel == artistLevel).Count();
            return collectionNews.OrderByDescending(x => x.CreatTime).Where(x => x.ArtistTypeID == artistTypeId && x.ArtistLevel == artistLevel).Skip((page - 1) * 5).Take(5).ToList();
        }

        #endregion

        #region  列表

        /// <summary>
        /// 新闻列表页
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public ActionResult Section(int id, int page = 1)
        {
            var curenSection = _isectionService.getNewsClassByID(id);
            var CurentSectionNews = _isectionService.getNewsClassByID(id).News.Where(x=>x.isDelete==0).ToList();
            var model = new List<News>();
            News hotNews = null;

            #region 位置导航 -学院动态-学院新闻
            NewsClass curentSectionParent = null;
            if (curenSection.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(curenSection.ParentID);
            }
            var nav = "";
            if (curentSectionParent != null)
            {
                ViewBag.navPic = curentSectionParent.NaviPIC;
                ViewBag.paentclassName = curentSectionParent.ClassName;
                nav += "-" + curentSectionParent.ClassName + "-" + curenSection.ClassName + "";
            }
            else
            {
                ViewBag.navPic = curenSection.NaviPIC;
                ViewBag.paentclassName = curenSection.ClassName;
                nav += "-" + curenSection.ClassName + "";
            }
            ViewData["nav"] = nav;

            #endregion



            ViewData["curentNewsClass"] = curenSection;
            ViewBag.curentPage = page;
            ViewBag.TottalCount = CurentSectionNews.Count;

            var toView = "";
            switch (curenSection.ShowWay)
            {
                case 0://普通文字
                    #region 普通文字列表第一页热点图文新闻
                    if (page == 1)//第一页显示热点图文
                    {
                        hotNews = CurentSectionNews.Where(x => x.IsHot == 1).OrderByDescending(x => x.NewsID).FirstOrDefault();
                        ViewData["hotNews"] = hotNews;
                    }
                    #endregion
                    model = GetNormalModel(page, CurentSectionNews, hotNews);
                    toView = "NormalSection";
                    break;
                case 1://图文列表新闻(可点击进详细页)
                    toView = "PicSection";
                    model = GetModel(page, CurentSectionNews,5);
                    break;
                case 2://图文列表 领导 （有详细页 ）
                    toView = "PicShowSection";
                    model = GetModel(page, CurentSectionNews, 15);
                    break;
                case 3://图文列表 风景展示（没有详细页 特效展示）
                    toView = "EffectsPicSection";
                    model = GetModel(page, CurentSectionNews,15);
                    break;






            }

            return View(toView, model);
        }


        

        /// <summary>
        /// 招生列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult SectionEnrol(int id, int page = 1)
        {
            var curenSection = _isectionService.getNewsClassByID(id);
            var CurentSectionNews = _isectionService.getNewsClassByID(id).News.Where(x=>x.isDelete==0).ToList();
            var model = new List<News>();
            News hotNews = null;

            #region 位置导航
            NewsClass curentSectionParent = null;
            if (curenSection.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(curenSection.ParentID);
            }
            var nav = "";
            if (curentSectionParent != null)
            {
                ViewBag.navPic = curentSectionParent.NaviPIC;
                ViewBag.paentclassName = curentSectionParent.ClassName;
                nav +=  curentSectionParent.ClassName + "-" + curenSection.ClassName + "";
            }
            else
            {
                ViewBag.navPic = curenSection.NaviPIC;
                ViewBag.paentclassName = curenSection.ClassName;
                nav +=  curenSection.ClassName + "";
            }
            ViewData["nav"] = nav;

            #endregion



            ViewData["curentNewsClass"] = curenSection;
            ViewBag.curentPage = page;
            ViewBag.TottalCount = CurentSectionNews.Count;

            var toView = "";
            switch (curenSection.ShowWay)
            {
                case 0://普通文字列表
                    #region 普通文字列表第一页热点图文新闻
                    if (page == 1)//第一页显示热点图文
                    {
                        hotNews = CurentSectionNews.Where(x => x.IsHot == 1).OrderByDescending(x => x.NewsID).FirstOrDefault();
                        ViewData["hotNews"] = hotNews;
                    }
                    #endregion
                    model = GetNormalModel(page, CurentSectionNews, hotNews);
                    toView = "NormalEnrolSection";
                    break;
                case 1://普通图文列表
                    toView = "PicEnrolSection";
                    model = GetModel(page, CurentSectionNews,5);
                    break;
                case 5://院系新闻列表
                    toView = "AcademySection";
                    model = GetModel(page, CurentSectionNews,15);
                    break;
            }

            return View(toView, model);
        }

        #endregion

        #region  新闻
        /// <summary>
        /// 普通新闻浏览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult news(int id)
        {
            var newEntiy = _inewsSevice.GetNewsByID(id);

            if (newEntiy.isDelete != 0)
            {
                return Redirect("/");
            }

            newEntiy.ClickNum = newEntiy.ClickNum + 1;
            _inewsSevice.UpdateNews(newEntiy);

            //当前根栏目名称
            var CurentNewsClass = _isectionService.getNewsClassByID(newEntiy.NewsClass.ClassID);
            //var parentClass = _isectionService.getNewsClassByID(newEntiy.NewsClass.ParentID);
            ViewBag.CurentNewsClass = CurentNewsClass;

            //ViewBag.parentClassName = parentClass.ClassName;

            #region 位置导航
            NewsClass curentSectionParent = null;
            if (newEntiy.NewsClass.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(newEntiy.NewsClass.ParentID);
            }
            var nav = "";
            if (curentSectionParent != null)
            {
                nav +=  curentSectionParent.ClassName + "-" + newEntiy.NewsClass.ClassName + "";
                ViewBag.parentClassName = curentSectionParent.ClassName;
            }
            else
            {
                nav +=  newEntiy.NewsClass.ClassName + "";
                ViewBag.parentClassName = CurentNewsClass.ClassName;
            }
            ViewData["nav"] = nav;
            #endregion

            News newsNextQuery = null;
            News newsPrevQuery = null;

            

            //newsNextQuery = _inewsSevice.GetAllNews().Where(x => x.CreatTime < newEntiy.CreatTime && x.ClassID == newEntiy.ClassID&&x.isDelete==0).OrderByDescending(x => x.CreatTime).FirstOrDefault();
            //newsPrevQuery = _inewsSevice.GetAllNews().Where(x => x.CreatTime > newEntiy.CreatTime && x.ClassID == newEntiy.ClassID&&x.isDelete==0).OrderBy(x => x.CreatTime).FirstOrDefault();

            //var nextNewsID= _isqlQueryService.GetNextOrPreNewsID(newEntiy.ClassID, newEntiy.NewsID, 2).FirstOrDefault();
            //if (nextNewsID > -1)
            //{
            //    newsNextQuery = _inewsSevice.GetNewsByID(nextNewsID);
            //}

            //var prevNewsID = _isqlQueryService.GetNextOrPreNewsID(newEntiy.ClassID, newEntiy.NewsID, 1).FirstOrDefault();
            //if (prevNewsID > -1)
            //{
            //    newsPrevQuery = _inewsSevice.GetNewsByID(nextNewsID);
            //}


            newsPrevQuery = getNextOrPrevNewsID(newEntiy.ClassID, newEntiy.NewsID, 1);
            newsNextQuery = getNextOrPrevNewsID(newEntiy.ClassID, newEntiy.NewsID, 2);



            if (newsNextQuery != null)
            {
                ViewData["nextNews"] = newsNextQuery;
            }
            if (newsPrevQuery != null)
            {
                ViewData["prevNews"] = newsPrevQuery;
            }
            return View(newEntiy);
        }

        /// <summary>
        /// 招生新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EnrolNews(int id)
        {
            var newEntiy = _inewsSevice.GetNewsByID(id);
            if (newEntiy.isDelete != 0)
            {
                return Redirect("/");
            }
            newEntiy.ClickNum = newEntiy.ClickNum + 1;
            _inewsSevice.UpdateNews(newEntiy);

            //当前根栏目名称
            var CurentNewsClass = _isectionService.getNewsClassByID(newEntiy.NewsClass.ClassID);
            //var parentClass = _isectionService.getNewsClassByID(newEntiy.NewsClass.ParentID);
            ViewBag.CurentNewsClass = CurentNewsClass;

            //ViewBag.parentClassName = parentClass.ClassName;

            #region 位置导航
            NewsClass curentSectionParent = null;
            if (newEntiy.NewsClass.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(newEntiy.NewsClass.ParentID);
            }
            var nav = "";
            if (curentSectionParent != null)
            {
                nav +=  curentSectionParent.ClassName + "-" + newEntiy.NewsClass.ClassName + "";
                ViewBag.parentClassName = curentSectionParent.ClassName;
            }
            else
            {
                nav +=  newEntiy.NewsClass.ClassName + "";
                ViewBag.parentClassName = CurentNewsClass.ClassName;
            }
            ViewData["nav"] = nav;
            #endregion

            News newsNextQuery = null;
            News newsPrevQuery = null;



            //newsNextQuery = _inewsSevice.GetAllNews().Where(x => x.CreatTime < newEntiy.CreatTime && x.ClassID == newEntiy.ClassID&&x.isDelete==0).OrderByDescending(x => x.CreatTime).FirstOrDefault();
            //newsPrevQuery = _inewsSevice.GetAllNews().Where(x => x.CreatTime > newEntiy.CreatTime && x.ClassID == newEntiy.ClassID && x.isDelete == 0).OrderBy(x => x.CreatTime).FirstOrDefault();


            newsPrevQuery = getNextOrPrevNewsID(newEntiy.ClassID, newEntiy.NewsID, 1);
            newsNextQuery = getNextOrPrevNewsID(newEntiy.ClassID, newEntiy.NewsID, 2);


            if (newsNextQuery != null)
            {
                ViewData["nextNews"] = newsNextQuery;
            }
            if (newsPrevQuery != null)
            {
                ViewData["prevNews"] = newsPrevQuery;
            }
            return View(newEntiy);
        }

        

        #endregion

        #region 单页
        /// <summary>
        /// 主站单页
        /// </summary>
        /// <param name="id">单页栏目id</param>
        /// <returns></returns>
        public ActionResult singlePage(int id)
        {
            var singlePageClass = _isectionService.getNewsClassByID(id);

            var parentClass = _isectionService.getNewsClassByID(singlePageClass.ParentID);


            #region 位置导航
            NewsClass curentSectionParent = null;
            if (singlePageClass.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(singlePageClass.ParentID);
            }
            var nav = "";
            ViewBag.parentName = "";
            if (curentSectionParent != null)
            {
                nav +=  curentSectionParent.ClassName + "-" + singlePageClass.ClassName + "";
                ViewBag.parentName = curentSectionParent.ClassName;
            }
            else
            {
                nav +=  singlePageClass.ClassName + "";
                ViewBag.parentName = singlePageClass.ClassName;
            }
            ViewData["nav"] = nav;
            #endregion

            return View(singlePageClass);
        }


        /// <summary>
        /// 招生专题单页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult singleEnrol(int id)
        {
            var singlePageClass = _isectionService.getNewsClassByID(id);

            var parentClass = _isectionService.getNewsClassByID(singlePageClass.ParentID);


            #region 位置导航
            NewsClass curentSectionParent = null;
            if (singlePageClass.ParentID != 0)
            {
                curentSectionParent = _isectionService.getNewsClassByID(singlePageClass.ParentID);
            }
            var nav = "";
            ViewBag.parentName = "";
            if (curentSectionParent != null)
            {
                nav +=  curentSectionParent.ClassName + "-" + singlePageClass.ClassName + "";
                ViewBag.parentName = curentSectionParent.ClassName;
            }
            else
            {
                nav += singlePageClass.ClassName + "";
                ViewBag.parentName = singlePageClass.ClassName;
            }
            ViewData["nav"] = nav;
            #endregion

            return View(singlePageClass);
        }


       

        #endregion

        #region 留言板

        //public ActionResult LeaveMessage()
        //{
        //    //var parentClass = _isectionService.getNewsClassByID(32);
        //    //ViewBag.navPic = parentClass.NaviPIC;
        //    return View();
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">页码</param>
        /// <returns></returns>
        //public ActionResult partialMessage(int id)
        //{
        //    var pageIndex = id - 1;
        //    var messages = _ileaveMessageService.getLeaveMessages(5, pageIndex);
        //    ViewBag.curentPage = id;
        //    ViewBag.TottalCount = messages.TotalCount;
        //    var model = messages.ToList();
        //    return View(model);
        //}


        /// <summary>
        /// 留言内容列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult MessageList(int id = 1)
        //{
        //    var pageIndex = id - 1;
        //    var messages = _ileaveMessageService.getLeaveMessages(5, pageIndex);
        //    ViewBag.curentPage = id;
        //    ViewBag.TottalCount = messages.TotalCount;
        //    var model = messages.ToList();
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult LeaveMessage(FormCollection form)
        //{
        //    //var workcontext = EngineContext.Current.Resolve<IWorkContext>();
        //    //var curentUser = workcontext.CurrentCustomer;
        //    //var message = new LeaveMessage();
        //    //message.IsShow = false;
        //    //message.LeaveContent = form["content"].ToString();
        //    //message.createTime = DateTime.Now;

        //    //if (curentUser != null)
        //    //{
        //    //    message.LeaveName = curentUser.UserName;
        //    //    message.QQ = curentUser.Email;
        //    //    message.Phone = curentUser.Mobile;
        //    //}
        //    //else
        //    //{
        //    //    try
        //    //    {
        //    //        message.LeaveName = form["leaveName"].ToString();
        //    //        message.QQ = form["leaveQQ"].ToString();
        //    //        message.Phone = form["leavePhone"].ToString();
        //    //    }
        //    //    catch
        //    //    {
        //    //        return Redirect("/Information/LeaveMessage");
        //    //    }

        //    //}
        //    //try
        //    //{
        //    //    _ileaveMessageService.insertMessage(message);
        //    //    return Json("OK");
        //    //}
        //    //catch
        //    //{
        //    //    return Json("NO");
        //    //}
        //    var message = new LeaveMessage();
        //    message.LeaveName = form["leaveName"].ToString();
        //    message.QQ = form["leaveEmail"].ToString();
        //    message.Phone = form["leavePhone"].ToString();
        //    message.LeaveContent = form["leaveContent"].ToString();
        //    message.createTime = DateTime.Now;
        //    _ileaveMessageService.insertMessage(message);
        //    return Redirect("/Information/MessageList");
        //}


        #endregion

        #region 手机

        /// <summary>
        /// 子栏目列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult subNewsClassMobile(int id)
        {
            var curentNewsClass = _isectionService.getNewsClassByID(id);
            ViewBag.paentclassName = curentNewsClass.ClassName;

            #region 取父栏目名称
            //NewsClass curentSectionParent = null;
            //if (curentNewsClass.ParentID != 0)
            //{
            //    curentSectionParent = _isectionService.getNewsClassByID(curentNewsClass.ParentID);
            //}
            //if (curentSectionParent != null)
            //{
            //    ViewBag.paentclassName = curentSectionParent.ClassName;
            //}
            //else
            //{
            //    ViewBag.paentclassName = curentNewsClass.ClassName;
            //}
            #endregion

            var model = _isectionService.GetSiblingNewsClass(id).ToList();
            return View(model);
        }

        /// <summary>
        /// 某栏目下新闻列表
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public ActionResult SectionMobile(int id, int page = 1)
        {
            var curenSection = _isectionService.getNewsClassByID(id);
            var CurentSectionNews = _isectionService.getNewsClassByID(id).News.Where(x=>x.isDelete==0).ToList();
            var model = new List<News>();

            ViewData["curentNewsClass"] = curenSection;
            ViewBag.curentPage = page;
            ViewBag.TottalCount = CurentSectionNews.Count;
            //model = CurentSectionNews.OrderBy(x => x.NewsID).Skip((page - 1) * 10).Take(10).ToList();
            //return View(model);

            var toView = "";
            if (curenSection.ShowWay == 3)
            {
                toView = "EffectsPicSection";
                ViewBag.MViewName = "EffectsPicSection";
                model = GetModel(page, CurentSectionNews,15);
            }
            else if (curenSection.ShowWay == 0)
            {
                model = GetModel(page, CurentSectionNews,15);
                ViewBag.MViewName = "NormalSection";
                toView = "NormalSection";
            }
            else if (curenSection.ShowWay == 20)//借阅指南
            {
                model = GetModel(page, CurentSectionNews,15);
                ViewBag.MViewName = "BorrowGuide";
                toView = "BorrowGuide";
            }
            else
            {
                toView = "PicSection";
                ViewBag.MViewName = "PicSection";
                    model = GetModel(page, CurentSectionNews,15);
            }


            return View(toView, model);
        }

        /// <summary>
        /// 新闻列表异步数据
        /// </summary>
        /// <param name="id">栏目id</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public JsonResult SectionMobileAjax(int id, int page)
        {

            var CurentSectionNews = _isectionService.getNewsClassByID(id).News.Where(x=>x.isDelete==0).ToList();
            var model = new List<News>();
            if (Request.QueryString["artistTypeId"] != null)
            {
                model = CurentSectionNews.Where(x => x.ArtistTypeID == int.Parse(Request.QueryString["artistTypeId"])).OrderByDescending(x => x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
            }
            else
            {
                model = CurentSectionNews.OrderByDescending(x => x.CreatTime).Skip((page - 1) * 15).Take(15).ToList();
            }

            return new JsonResult() { Data = model.Select(x => new { PicURL = x.PicURL, NewsContent = System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(x.NewsContent, "<[^>]+>", ""), "&[^;]+;", ""), NewsTitle = x.NewsTitle, NewsID = x.NewsID, CreatTime = x.CreatTime.ToString("yyyy-MM-dd") }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }







        #endregion

    }
}
