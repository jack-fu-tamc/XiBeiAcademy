using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public partial class LeaveMessage : BaseEntity
    {
        public int MessageID { get; set; }
        public string LeaveName { get; set; }
        public string QQ { get; set; }
        public string Phone { get; set; }
        public string LeaveContent { get; set; }
        public bool IsShow { get; set; }
        public string Answer { get; set; }
        //public Nullable<System.DateTime> createTime { get; set; }
        //public Nullable<System.DateTime> answerTime { get; set; }

        public System.DateTime createTime { get; set; }
        public System.DateTime answerTime { get; set; }
    }
}
