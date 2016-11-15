using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HC.Service.Account;
using HC.Service.News;
using HC.Service.Section;
using Maticsoft.Model;
using HC.Data.ViewModels;
using HC.Data.Models;

namespace HCHEv2.Controllers
{
    public class HomeController : Controller
    {
        #region field
        private IUser_InfoService iuserService;
        private INewsService _InewsService;
        private ISectionService _IsectionService;
        #endregion

        #region cto
        public HomeController(IUser_InfoService Iuser_service,INewsService inewsService,ISectionService isectionService )
        {
            this.iuserService = Iuser_service;
            this._InewsService = inewsService;
            this._IsectionService = isectionService;
        }
        #endregion

        public ActionResult Index()
        {
            var isMobile = Request.Browser.IsMobileDevice || Request.UserAgent.ToLower().Contains("micromessenger");
            var model = new IndexModel();          
           
            model.BigPicNews = _InewsService.getRecNewsByClassID(7, 3,true);
            model.ImportantNews = _InewsService.getNewsByClassID(2067, 9,false,0);
            model.SchooleActivitiesNews = _InewsService.getNewsByClassID(2068, 9,false,0);
            model.EducationNews = _InewsService.getNewsByClassID(1, 7, false, 0);

            model.EmploymentNews = _InewsService.getNewsByClassID(29, 5, true, 1);
            model.EmploymentRecNews = _InewsService.getRecNewsByClassID(29, 1, true).FirstOrDefault();


            model.CertificationNews = _InewsService.getNewsByClassID(43, 5,true,1);
            model.CertificationRecNews = _InewsService.getRecNewsByClassID(43, 1, true).FirstOrDefault();

            model.TeacherBearingNews = _InewsService.getNewsByClassID(32, 3, true, 2);
            if (isMobile)
            {
                model.TeacherBearingRecNews = _InewsService.getRecNewsByClassID(32, 1, true);
            }
            else
            {
                model.TeacherBearingRecNews = _InewsService.getRecNewsByClassID(32, 2, true);
            }
           

            model.SchooleLifeNews = _InewsService.getRecNewsByClassID(47, 5,true);

            #region 手机首页通知公告
            if (isMobile)
            {
                var exceptClassIds = new List<int>();
                exceptClassIds.Add(2081);
                ViewBag.DeliveryNews = _InewsService.GetLeftDeliveryNews(5, exceptClassIds).ToList();

                ViewBag.DianZhuanNews = _InewsService.getNewsByClassID(7, 5, false, 0);

            }
            #endregion

            return View(model);
        }


    


        public ActionResult Enrol()
        {

            ViewBag.tzggLast = _InewsService.getRecNewsByClassID(3088, 1, true).ToList().FirstOrDefault();//招生首页通知公告 推荐
            ViewBag.tzgg = _InewsService.getNewsByClassID(3088, 6, false, 1).ToList();//招生首页 通知公告 最新6条 除过推荐
            ViewBag.rollNews = _InewsService.getNewsByClassID(3089, 5, false, 0).ToList();//招生首页 滚动新闻
            ViewBag.bkbd = _InewsService.getNewsByClassID(3095, 3, false, 0).ToList();//招生首页 报考必读


            return View();
        }


        public ActionResult deny()
        {
            return View();
        }


        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        //public ActionResult SearchContent(string sKey,int page,string palcehold)
        //{
        //    if (page == 0)
        //    {
        //        page = 1;
        //    }
        //    var model = new List<HC.Data.Models.News>();
        //    var query = _InewsService.SearchNews(sKey, 8, page);

        //    ViewBag.curentPage=page;
        //    ViewBag.TottalCount = query.TotalCount;
        //    model = query.ToList();
        //    return View(model);
        //}

        /// <summary>
        /// 页面头部菜单导航
        /// </summary>
        /// <returns></returns>
        public ActionResult HeadNav()
        {

            var NewsClass = _IsectionService.getAllSection();           
            var levelOne = NewsClass.SectionLists.Where(x => x.ParentID == 0&&x.IsShowInNav==1).OrderBy(x=>x.ClassOrder);
            return PartialView(levelOne.ToList());
        }


        


        /// <summary>
        /// 图书馆头部导航
        /// </summary>
        /// <returns></returns>
        public ActionResult HeadNavLibrary()
        {
            var NewsClass = _IsectionService.getAllSection();
            var levelOne = NewsClass.SectionLists.Where(x => x.ParentID == 2077 && x.IsShowInNav == 1).OrderBy(x => x.ClassOrder);
            return PartialView(levelOne.ToList());
        }

        /// <summary>
        /// 教育教学头部导航
        /// </summary>
        /// <returns></returns>
        public ActionResult HeadNavEducation()
        {
            var NewsClass = _IsectionService.getAllSection();
            var levelOne = NewsClass.SectionLists.Where(x => x.ParentID == 1 && x.IsShowInNav == 1).OrderBy(x => x.ClassOrder);
            return PartialView(levelOne.ToList());
        }
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        //public ActionResult linkAddress()
        //{
        //    return PartialView();
        //}
    }
}
