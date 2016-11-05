using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HC.Data.Models;
using HC.Data.ViewModels;
using Telerik.Web.Mvc;

namespace HC.Data.ViewModels
{
    public class LeaveMesModelList
    {
        public LeaveMesModelList()
        {
            LeaveMesModels = new GridModel<LeaveMessaeModel>();
        }
        public GridModel<LeaveMessaeModel> LeaveMesModels { get; set; }
    }

    public class LeaveMessaeModel
    {
        public int MessageID { get; set; }
        public string LeaveName { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        public string LeaveContent { get; set; }
        public bool IsShow { get; set; }
        public string Answer { get; set; }
        public string createTime { get; set; }
        public string answerTime { get; set; }    
    }
}
