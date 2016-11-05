using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Web.Mvc;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class LogResultViewModel
    {
        public LogResultViewModel()
        {
            this.Logs = new GridModel<OpLog>();
        }
        public GridModel<OpLog> Logs { get; set; }

    }
     
    public class SerchLogModel
    {               
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int UserID { get; set; }
    }

}
