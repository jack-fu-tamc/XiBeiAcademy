using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Model;
using System.Text;
using System.Data;

using HC.Service.Account;
using HC.Service;
using HC.Service.Section;
using HC.Service.User;
using HC.Core;
using HC.Core.CommonMethod;
using HC.Core.Common.FileUpload;

using HC.Web.Framework;
using System.Collections;


namespace HC.Manage.Controllers
{
    public class CommonController : ManageBaseController
    {
        #region fild
      
        private IWorkContext _IWorkContext;
        private ISectionService _isectionService;
        private IUser_InfoService _iuser_infoService;
        private IUserService _iuserService;
        #endregion

        #region cto
        public CommonController(IUserService iuserService, IWorkContext iworkContext, IUser_InfoService iuser_infoService, ISectionService isectionService)
        {
            this._iuserService = iuserService;
            this._IWorkContext = iworkContext;
            this._isectionService = isectionService;
            this._iuser_infoService = iuser_infoService;
        }
        #endregion
       



      






        /// <summary>
        /// 主站后台头部搜索
        /// </summary>
        /// <returns></returns>
        //public ActionResult centerSerchAB()
        //{
        //    return PartialView();
        //}

        //public ActionResult centerNavMenuAB()
        //{
        //    var curentUser = _IWorkContext.CurrentCustomer;
        //    var userIDMD5 = UntilMethod.Md5Encrypt(curentUser.Id.ToString());

        //    ViewData["userIDMD5"] = userIDMD5;
        //    return PartialView();
        //}


       


       


        /// <summary>
        /// 下载商品导入模板
        /// </summary>
        /// <returns></returns>
        public FileResult DownTemplet()
        {
            var ver = Request.QueryString["ver"].ToString();
            if(ver=="2003")
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/HC.Manage/ExcelTempDown/2003excel.xls");
                //return File(path, "application/octet-stream", "产品导入Excel模板2003.xls");
                return File(path, "application/ms-excel", "产品导入Excel模板2003.xls");
            }
            else
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/HC.Manage/ExcelTempDown/2007excel.xlsx");
                //return File(path, "application/octet-stream", "产品导入Excel模板2007.xlsx");
                return File(path, "application/ms-excel", "产品导入Excel模板2007.xlsx");
            }
        }





        /// <summary>
        /// 店铺形象文件上传
        /// </summary>
        /// <returns></returns>
        [imgFileter]
        [NoFilter]
        [HttpPost]
        public ActionResult fileUp()
        {
            #region
            var file = Request.Files["Filedata"];
            //var userID = Request.QueryString["u"];
            var foldName = Request.QueryString["foldName"].ToString();


            if (file != null)
            {
                imgUpload imgUpLoad = new imgUpload();//上传图片的类   
                var reUrl = imgUpLoad.picUpLoad(file, foldName);
                //return new ContentResult() { Content = serverPath.imageURL + reUrl, ContentType = "text/plain", ContentEncoding = System.Text.Encoding.UTF8 };
                return new ContentResult() { Content = reUrl, ContentType = "text/plain", ContentEncoding = System.Text.Encoding.UTF8 };
            }
            else
            {
                return new ContentResult() { Content = "0", ContentType = "text/plain", ContentEncoding = System.Text.Encoding.UTF8 };
            }
            #endregion

        }

      

        /// <summary>
        /// 产品图片上传
        /// </summary>
        /// <returns></returns>
        [imgFileter]
        [NoFilter]
        [HttpPost]
        public ActionResult fileUpOfPro()
        {
            #region
            HttpPostedFileBase file = Request.Files["imgFile"];
            var foldName = Request.QueryString["foldName"].ToString();

            Hashtable hash = new Hashtable();
            if (file != null)
            {
                imgUpload imgUpLoad = new imgUpload();//上传图片的类   
                var reUrl = imgUpLoad.picUpLoad(file, foldName);
                hash["error"] = 0;
                hash["url"] = reUrl;
                return Json(hash, "text/html;charset=UTF-8");
            }
            else
            {
                hash["error"] = 1;
                hash["message"] = "请选择文件";
                return Json(hash);
            }
            #endregion

        }


        public ActionResult transferOrCopyNews(int id)
        {
           

             var curentUser = _iuserService.getUserByID(id);
            var sectionPermission = curentUser.UserGroup.SectionPermission;
            List<string> sectionIDstr = new List<string>(sectionPermission.Split(','));

            var sectionIDs = sectionIDstr.Select(x => Convert.ToInt32(x)).ToList();
            ViewBag.sectionIDs = sectionIDs;

           
          
            return View();
        }


        //public ActionResult showFiles(string dir)
        //{
            
        //}



        //public ActionResult getRescouce()
        //{
        //    var dirName = Request.Form["dirPath"];
        //    var pageIndex = Request.Form["pIndex"];
        //    var pageSize = 30;
        //    var fileType = Request.Form["dirDumm"];

        //}
    }
}
