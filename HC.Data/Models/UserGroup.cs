using System;
using System.Collections.Generic;

namespace HC.Data.Models
{
    public partial class UserGroup : BaseEntity
    {
        public UserGroup()
        {
            this.CxUsers = new List<CxUser>();
        }

        public int UserGroupID { get; set; }
        public string GroupName { get; set; }
        public string Comments { get; set; }
        public string SectionPermission { get; set; }
        public string ManagePermission { get; set; }
        public virtual ICollection<CxUser> CxUsers { get; set; }
    }
}
