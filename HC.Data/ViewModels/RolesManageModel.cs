using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using Telerik.Web.Mvc;



namespace HC.Data.ViewModels
{
    public class RolesManageModel
    {
        public RolesManageModel()
        {
            this.Roels = new GridModel<UserGroup>();
        }

        public GridModel<UserGroup> Roels { get; set; }
    }
}
