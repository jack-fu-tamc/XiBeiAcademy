using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Web.Mvc;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class BmStudentListModel
    {
        public BmStudentListModel()
        {
            students = new GridModel<StudentInfo>();
        }

        public GridModel<StudentInfo> students { get; set; }
    }

    public class SerchStudentModel
    {
        public string StFrom { get; set; }
        public string StCardNo { get; set; }
        public string StudentName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }

        public DateTime sTime { get; set; }
        public DateTime eTime { get; set; }
        //public int sortType { get; set; }
    }
}
