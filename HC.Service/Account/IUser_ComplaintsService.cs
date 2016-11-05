using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Service.Account
{
    public interface IUser_ComplaintsService
    {
        #region  BasicMethod

        //select 
        DataTable GetList(Maticsoft.Model.User_Complaints_Search model);

        //model
        Maticsoft.Model.User_Complaints Get(Maticsoft.Model.User_Complaints model);

        //add 
        int Add(Maticsoft.Model.User_Complaints model);

        // update
        int Update(Maticsoft.Model.User_Complaints model);

        #endregion  BasicMethod
    }
}
