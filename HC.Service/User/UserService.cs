using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.User
{
    public class UserService:IUserService
    {

        private IRepository<CxUser> _IrepositoryUser;

        public UserService(IRepository<CxUser> irepositoryUser)
        {
            this._IrepositoryUser = irepositoryUser;
        }

        public void UpdateUser(CxUser entiy)
        {
            _IrepositoryUser.Update(entiy);
        }

        public void AddUser(CxUser entity)
        {
            _IrepositoryUser.Insert(entity);
        }

        public CxUser getUserByID(int userID)
        {
            return _IrepositoryUser.GetById(userID);
        }

        public CxUser GetUserByName(string name)
        {
            var query = _IrepositoryUser.Table;
            return query.Where(x => x.UserName == name).FirstOrDefault();
        }

        public List<CxUser> GetAllUsers()
        {
            return _IrepositoryUser.Table.ToList();
        }

        public void DeleteUser(CxUser entity)
        {
            _IrepositoryUser.Delete(entity);
        }
    }
}
