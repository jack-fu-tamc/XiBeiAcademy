using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HC.Data.Models
{
    public partial class NewsClass : BaseEntity
    {
        public NewsClass()
        {
            this.Attachments = new List<Attachment>();
            this.News = new List<News>();
        }

        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int ParentID { get; set; }
        [DisplayName("栏目权重")]
        public Nullable<int> ClassOrder { get; set; }
        public string NaviPIC { get; set; }
        public string SmallPic { get; set; }
        public string NaviContent { get; set; }
        public string PageContent { get; set; }
        public int IsSingle { get; set; }
        public int IsShowInNav { get; set; }
        /// <summary>
        /// 列表展示方式
        /// </summary>
        public int ShowWay { get; set; }
        public string linkAddress { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
