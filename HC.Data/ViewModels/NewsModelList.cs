using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Web.Mvc;
using HC.Data.Models;

namespace HC.Data.ViewModels
{
    public class NewsModelList
    {
        public NewsModelList()
        {
            news = new GridModel<NewsWithNotNewsClass>();
        }
        public GridModel<NewsWithNotNewsClass> news { get; set; }
    }

    public class SerchNewsModel
    {
        public int NewsClassID { get; set; }
        public int searchType { get; set; }
        public string searchKey { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int sortType { get; set; }
    }

    public class NewsWithNotNewsClass
    {
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public int ClassID { get; set; }
        public int NewsOrder { get; set; }
        public string Author { get; set; }
        public string NewsContent { get; set; }
        public string PicURL { get; set; }
        public int NewsType { get; set; }
        public Nullable<int> NewsTitleCount { get; set; }
        public Nullable<int> contentCount { get; set; }
        public Nullable<int> isDelete { get; set; }
        public string CreatTime { get; set; }
        public string DeleteTime { get; set; }
        public Nullable<int> ClickNum { get; set; }
        public Nullable<int> IsHot { get; set; }
        public Nullable<int> IsRec { get; set; }
        /// <summary>
        /// 所属栏目名称
        /// </summary>
        public string className { get; set; }
    }
}
