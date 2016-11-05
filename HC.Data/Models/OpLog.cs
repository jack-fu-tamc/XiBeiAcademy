using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Data.Models
{
    public partial class OpLog:BaseEntity
    {
        public System.Guid Id { get; set; }
        public int UserID { get; set; }
        public string UserRealName { get; set; }
        public string UserAccount { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string OpDescriptions { get; set; }
        public string OpResult { get; set; }
    }
}
