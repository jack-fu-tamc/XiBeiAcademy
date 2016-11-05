using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using HC.Data.ViewModels;
using ResposityOfEf;

namespace HC.Service.OpLog
{
    public interface IopLogService
    {
        void AddOpLog(HC.Data.Models.OpLog entity);
        IPagedList<HC.Data.Models.OpLog> GetAllLogs( int pagesize, int pageIndex, int UserID, DateTime startTime, DateTime endTime);

        HC.Data.Models.OpLog getByName(string uName);
    }
}
