using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class IndexModel
    {
        public IndexModel()
        {

            this.BigPicNews = new List<News>();
            this.ImportantNews = new List<News>();

            this.SchooleActivitiesNews = new List<News>();
            this.EducationNews = new List<News>();
            this.EmploymentNews = new List<News>();
            this.CertificationNews = new List<News>();
            this.TeacherBearingNews = new List<News>();
            this.SchooleLifeNews = new List<News>();
            this.TeacherBearingRecNews = new List<News>();
          

        }
        /// <summary>
        /// 首页左侧图片新闻
        /// </summary>
        public IList<News> BigPicNews { get; set; }
        /// <summary>
        /// 重要新闻
        /// </summary>
        public IList<News> ImportantNews { get; set; }
        /// <summary>
        /// 校园动态
        /// </summary>
        public IList<News> SchooleActivitiesNews { get; set; }
        /// <summary>
        /// 教育教学
        /// </summary>
        public IList<News> EducationNews { get; set; }
        /// <summary>
        /// 招生就业
        /// </summary>
        public IList<News> EmploymentNews { get; set; }
        /// <summary>
        /// 招生就业推荐
        /// </summary>
        public News EmploymentRecNews { get; set; }
        /// <summary>
        /// 资格认证
        /// </summary>
        public IList<News> CertificationNews { get; set; }
        /// <summary>
        /// 资格认证推荐
        /// </summary>
        public News CertificationRecNews { get; set; }
        /// <summary>
        /// 名师风采
        /// </summary>
        public IList<News> TeacherBearingNews { get; set; }
        /// <summary>
        /// 名师风采推荐
        /// </summary>
        public IList<News> TeacherBearingRecNews { get; set; }
        /// <summary>
        /// 校园生活
        /// </summary>
        public IList<News> SchooleLifeNews { get; set; }

        
    }
}
