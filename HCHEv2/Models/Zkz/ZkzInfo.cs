using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HC.Data.Models;

namespace HCHEv2.Models.Zkz
{
    public class ZkzInfo
    {
        public ZkzInfo()
        {
            this.stuInfo = new StudentInfo();
        }
        public StudentInfo stuInfo { get; set; }


        /// <summary>
        /// 准考证号
        /// </summary>
        public string ZkzNo { get; set; }

        /// <summary>
        /// 考点名称
        /// </summary>
        public string ExamPlace { get; set; }

        /// <summary>
        /// 考场号
        /// </summary>
        public string ExamNo { get; set; }

        /// <summary>
        /// 教室号
        /// </summary>
        public string RoomNo { get; set; }

        /// <summary>
        /// 座位号
        /// </summary>
        public string SiteNo { get; set; }

        /// <summary>
        /// 考试时间
        /// </summary>
        public string ExamTime { get; set; }


        /// <summary>
        /// 具体科目和时间
        /// </summary>
        public string SubjectAndTime { get; set; }

    }
}