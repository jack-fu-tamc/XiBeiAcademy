using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using HC.Data.ViewModels;
using HC.Web.Framework;
using HC.Data.Models;
using HC.Service.EnrolSys;

namespace HC.Manage.Controllers
{
    public class EnrolSysController : ManageBaseController
    {
        //
        // GET: /EnrolSys/

        #region fild
        private IEnrolSysService _iEnrolSysService;
        #endregion

        #region cto
        public EnrolSysController(IEnrolSysService enrolsysService)
        {
            this._iEnrolSysService = enrolsysService;
        }
        #endregion


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public ActionResult BmList()
        {
            var model = new BmStudentListModel();
            return View(model);
        } 


        [HttpPost]
        public ActionResult BmListAjax(GridCommand cmd,SerchStudentModel sModel)
        {

            #region 时间初始化
            sModel.sTime = new DateTime(1949, 1, 1, 0, 0, 0);
            sModel.eTime = DateTime.Now;
            if (!string.IsNullOrEmpty(sModel.startTime))
            {
                sModel.sTime = Convert.ToDateTime(sModel.startTime);
            }


            if (!string.IsNullOrEmpty(sModel.endTime))
            {
                sModel.eTime = Convert.ToDateTime(sModel.endTime).AddMinutes(1439);
            }
            #endregion

            var queryData = _iEnrolSysService.getStudentInfoBySearch(sModel, cmd.PageSize, cmd.Page-1);
            var PageList = queryData.ToList();

            var result = new GridModel<StudentInfo>()
            {
                Data = PageList,
                Total = queryData.TotalCount                
            };
            //return this.Json()
            //return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return new VMEJsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}
