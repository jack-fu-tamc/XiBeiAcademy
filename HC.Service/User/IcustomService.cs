using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model;
using HC.Core.Enum;

namespace HC.Service.User
{
    public interface IcustomService
    {
        User_Info GetUserByID(int id);

        User_Info GetCustomerByUsername(string userName);

        CustomerLoginResults ValidateCustomer(string userName, string passWord);

        bool GetIsCommercial(int userId);
    }
}
