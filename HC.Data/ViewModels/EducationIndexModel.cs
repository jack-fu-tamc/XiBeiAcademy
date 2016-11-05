using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;


namespace HC.Data.ViewModels
{
    public class EducationIndexModel
    {
        public EducationIndexModel()
        {
            bigPic = new List<News>();
            operational = new List<News>();
            reformation = new List<News>();
            cooperation = new List<News>();
            continewEducation = new List<News>();
            demonstration = new List<News>();
            assessment = new List<News>();
            diagnose = new List<News>();
            download = new List<News>();
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduce { get; set; }
        /// <summary>
        /// 大图新闻
        /// </summary>
        public IList<News> bigPic { get; set; } 
        /// <summary>
        /// 教学运行
        /// </summary>
        public IList<News> operational { get; set; }
        public News oprerationalRec { get; set; } 
        /// <summary>
        /// 教学改革
        /// </summary>
        public IList<News> reformation { get; set; }
        public News reformationRec { get; set; }

        /// <summary>
        /// 合作办学
        /// </summary>
        public IList<News> cooperation { get; set; }
       
        /// <summary>
        /// 继续教育
        /// </summary>
        public IList<News> continewEducation { get; set; }
        public News continewEducationRec { get; set; }
        /// <summary>
        /// 示范建设
        /// </summary>
        public IList<News> demonstration { get; set; }
        public News demonstrationRec { get; set; }

        /// <summary>
        /// 评估专栏 
        /// </summary>
        public IList<News> assessment { get; set; }
        public News assessmentRec { get; set; }

        /// <summary>
        /// 诊断与改进
        /// </summary>
        public IList<News> diagnose { get; set; }
        public News diagnoseRec { get; set; }

        /// <summary>
        /// 资源下载
        /// </summary>
        public IList<News> download { get; set; }
    }
}
