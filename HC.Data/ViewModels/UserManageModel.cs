using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using Telerik.Web.Mvc;

namespace HC.Data.ViewModels
{
    public class UserManageModel
    {
        public UserManageModel()
        {
            this.Users = new GridModel<CxUser>();
        }

        public GridModel<CxUser> Users { get; set; }
    }
}
