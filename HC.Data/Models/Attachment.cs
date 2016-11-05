using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public partial class Attachment : BaseEntity
    {
        public int AttachmentID { get; set; }
        /// <summary>
        /// 1 œ‡≤·  2 ”∆µ
        /// </summary>
        public int AttaType { get; set; }
        public string AttaUrl { get; set; }
        public int NewsID { get; set; }
        public int AttaOrders { get; set; }
        public string AttaDescribe { get; set; }
        public string OriginalName { get; set; }
        public Nullable<int> ClassID { get; set; }
        public virtual News News { get; set; }
        public virtual NewsClass NewsClass { get; set; }
    }
}
