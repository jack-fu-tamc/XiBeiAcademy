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
using System.IO;
using System.Data;
using System.Reflection;

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


        #region common
        private DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
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



        [HttpGet]
        public ActionResult StudentDetail(int id)
        {
            var model = _iEnrolSysService.GetStudentInfoById(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult ExportStudent(SerchStudentModel sModel)
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

            var studentList = _iEnrolSysService.ExportStudentInfos(sModel);

          
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Excel/expStudent.xlsx");
            string templetPath = System.Web.HttpContext.Current.Server.MapPath("~/Excel/tempExpStudent.xlsx");
            FileInfo info = new FileInfo(path);
            FileInfo temp = new FileInfo(templetPath);
            var exp = new HC.Core.Common.ExportExcel.ExportExcel();
            exp.ExportExcel(info, temp, ToDataTable<StudentInfo>(studentList.ToList()));
            return File(path, "application/octet-stream", "人口信息导出.xlsx");
        }

    }
}
