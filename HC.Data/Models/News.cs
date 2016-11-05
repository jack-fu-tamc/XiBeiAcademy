using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HC.Data.Models
{
    public partial class News : BaseEntity
    {
        public News()
        {
            this.Attachments = new List<Attachment>();
        }

        public int NewsID { get; set; }
        [Required(ErrorMessage = "请填写新闻标题")]
        public string NewsTitle { get; set; }
        public int ClassID { get; set; }
        [Required(ErrorMessage = "请填写权重")]
        [DisplayName("权重")]
        public int NewsOrder { get; set; }
        public string Author { get; set; }
        public string NewsContent { get; set; }
        public string PicURL { get; set; }
        public Nullable<int> NewsType { get; set; }
        public int NewsTitleCount { get; set; }
        public int contentCount { get; set; }
        public int isDelete { get; set; }
        public System.DateTime CreatTime { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
        public int ClickNum { get; set; }
        public int IsHot { get; set; }
        public int IsRec { get; set; }
        /// <summary>
        /// 一级 二级..
        /// </summary>
        public Nullable<int> ArtistLevel { get; set; }
        /// <summary>
        /// 演员 编剧  剧种...
        /// </summary>
        public Nullable<int> ArtistTypeID { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string DeputyTitle { get; set; }
        
        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual NewsClass NewsClass { get; set; }
    }
}
