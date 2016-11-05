using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Service.User
{
    public interface IUserService
    {
        void UpdateUser(CxUser entiy);
        void AddUser(CxUser entity);
        CxUser getUserByID(int userID);
        CxUser GetUserByName(string name);
        List<CxUser> GetAllUsers();

        void DeleteUser(CxUser entity);
    }
}
