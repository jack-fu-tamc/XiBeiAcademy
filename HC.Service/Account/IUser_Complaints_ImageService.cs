using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Service.Account
{
    public interface IUser_Complaints_ImageService
    {
        #region  BasicMethod

        //select 
        DataTable GetList(Maticsoft.Model.User_Complaints_Image_Search model);

        //add 
        int Add(Maticsoft.Model.User_Complaints_Image model);

        // update
        int Update(Maticsoft.Model.User_Complaints_Image model);

        #endregion  BasicMethod
    }
}
