using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model;
using HC.Core.Enum;

namespace HC.Service.User
{
    public class CustomerService : IcustomService
    {

        public User_Info GetUserByID(int id)
        {
            //return null;
            return new User_Info()
            {
                Id = 1,
                //NickName = "jack",
                Email = "910836983@qq.com",
                UserName = "FJ"
                //DataStatus = 1
            };
        }

        public User_Info GetCustomerByUsername(string userName)
        {
            return new User_Info()
            {
                Id = 1,
                //NickName = "jack",
                Email = "910836983@qq.com",
                UserName = "FJ"
                //DataStatus = 1
            };
        }

        public CustomerLoginResults ValidateCustomer(string userName, string passWord)
        {
            return CustomerLoginResults.Successful;
        }

        public bool GetIsCommercial(int userId)
        {
            return true;
        }
    }
}
