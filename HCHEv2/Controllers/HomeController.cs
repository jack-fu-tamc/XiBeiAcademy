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


        #region 图书馆

        public ActionResult Library()
        {
            var model = new libraryIndexModel();

            model.bigPicNews = _InewsService.getRecNewsByClassID(2079, 3, true);
            model.borrowGuide = _InewsService.getNewsByClassID(2081, 3, false, 0);
            model.report = _InewsService.getNewsByClassID(2079, 5, true, 1);
            model.reportRec = _InewsService.getRecNewsByClassID(2079, 1, true).FirstOrDefault();

            model.introduce = _IsectionService.getNewsClassByID(2078).PageContent ?? "";
            model.introduce = System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(model.introduce, "<[^>]+>", ""), "&[^;]+;", "");

            if (model.introduce.Length > 140)
            {
                model.introduce = model.introduce.Substring(0, 140) + "...";
            }
            return View(model);
        }
        #endregion


        #region 教育教学
        public ActionResult Education()
        {
            var model = new EducationIndexModel();

            model.bigPic = _InewsService.getRecNewsByClassID(1, 3, true);
            model.operational = _InewsService.getNewsByClassID(3, 5, true, 1);
            model.oprerationalRec = _InewsService.getRecNewsByClassID(3, 1, true).FirstOrDefault();

            model.reformation = _InewsService.getNewsByClassID(5, 5, true, 1);
            model.reformationRec = _InewsService.getRecNewsByClassID(5, 1, true).FirstOrDefault();

            model.cooperation = _InewsService.getNewsByClassID(2072, 7, false, 0);

            model.continewEducation = _InewsService.getNewsByClassID(2, 5, true, 1);
            model.continewEducationRec = _InewsService.getRecNewsByClassID(2, 1, true).FirstOrDefault();

            model.demonstration = _InewsService.getNewsByClassID(4, 5, true, 1);
            model.demonstrationRec = _InewsService.getRecNewsByClassID(4, 1, true).FirstOrDefault();

            model.assessment = _InewsService.getNewsByClassID(2070, 5, true, 1);
            model.assessmentRec = _InewsService.getRecNewsByClassID(2070, 1, true).FirstOrDefault();

            model.diagnose  = _InewsService.getNewsByClassID(2071, 5, true, 1);
            model.diagnoseRec = _InewsService.getRecNewsByClassID(2071, 1, true).FirstOrDefault();

            model.download = _InewsService.getNewsByClassID(2073, 7, false, 0);

            model.introduce = _IsectionService.getNewsClassByID(2069).PageContent ?? "";
            model.introduce = System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(model.introduce, "<[^>]+>", ""), "&[^;]+;", "");

            if (model.introduce.Length > 143)
            {
                model.introduce = model.introduce.Substring(0, 143) + "...";
            }



            return View(model);
        }
        #endregion


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
