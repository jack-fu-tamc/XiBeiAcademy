using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.UserGroup
{
    public class UserGroupService:IuserGroupService
    {
        private IRepository<HC.Data.Models.UserGroup> _iRepositoryUserGroup;
        public UserGroupService(IRepository<HC.Data.Models.UserGroup> irepositoryUserGroup){
            this._iRepositoryUserGroup=irepositoryUserGroup;
        }

        public List<HC.Data.Models.UserGroup> GetAllGroup()
        {
            return _iRepositoryUserGroup.Table.ToList();
        }

        public HC.Data.Models.UserGroup GetByID(int userGroupID)
        {
            return _iRepositoryUserGroup.GetById(userGroupID);
        }

        public void AddEntity(HC.Data.Models.UserGroup entity)
        {
            _iRepositoryUserGroup.Insert(entity);
        }
        public void UpdateEntity(HC.Data.Models.UserGroup entity)
        {
            _iRepositoryUserGroup.Update(entity);
        }

        public void DeleteEntity(HC.Data.Models.UserGroup entity)
        {
            _iRepositoryUserGroup.Delete(entity);
        }
    }
}
