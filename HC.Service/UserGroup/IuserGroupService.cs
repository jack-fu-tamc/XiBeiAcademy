using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;

namespace HC.Service.UserGroup
{
    public interface IuserGroupService
    {
        List<HC.Data.Models.UserGroup> GetAllGroup();
        HC.Data.Models.UserGroup GetByID(int userGroupID);
        void AddEntity(HC.Data.Models.UserGroup entity);
        void UpdateEntity(HC.Data.Models.UserGroup entity);
        void DeleteEntity(HC.Data.Models.UserGroup entity);
    }
}
