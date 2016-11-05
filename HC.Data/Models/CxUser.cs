using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public partial class CxUser : BaseEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool isAdmin { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int UserGroupID { get; set; }
        public bool Effective { get; set; }
        public DateTime passWordTime { get; set; }
        public Nullable<DateTime> LockTime { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
