using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class libraryIndexModel
    {
        public libraryIndexModel()
        {
            report = new List<News>();
            bigPicNews = new List<News>();
            borrowGuide = new List<News>();
        }


        public string introduce { get; set; }
        /// <summary>
        /// 新闻播报
        /// </summary>
        public IList<News> report { get; set; }

        public News reportRec { get; set; }
        /// <summary>
        /// 大图新闻
        /// </summary>
        public IList<News> bigPicNews { get; set; }
        /// <summary>
        /// 借阅指南
        /// </summary>
        public IList<News> borrowGuide { get; set; }
    }
}
