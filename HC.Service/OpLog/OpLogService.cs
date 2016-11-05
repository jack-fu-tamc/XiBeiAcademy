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
    public class OpLogService : IopLogService
    {

        #region fileds
        private IRepository<HC.Data.Models.OpLog> _OpRepository;       
        #endregion

        #region cto
        public OpLogService(IRepository<HC.Data.Models.OpLog> OpRepository)
        {
            this._OpRepository = OpRepository;           
        }
        #endregion

        public void AddOpLog(Data.Models.OpLog entity)
        {
            this._OpRepository.Insert(entity);
        }

        public IPagedList<Data.Models.OpLog> GetAllLogs(int pagesize, int pageIndex, int UserID, DateTime startTime, DateTime endTime)
        {
            var query = this._OpRepository.Table; 
            if (UserID > 0)
            {
                query = query.Where(x => x.UserID == UserID);
            }
            query = query.Where(x => x.CreateTime >= startTime && x.CreateTime <= endTime).OrderByDescending(x=>x.CreateTime);
            return new PagedList<HC.Data.Models.OpLog>(query, pageIndex, pagesize);
        }

        public HC.Data.Models.OpLog getByName(string uName)
        {
            var query= _OpRepository.Table.OrderByDescending(x=>x.CreateTime);
            return query.Where(x => x.UserAccount == uName).FirstOrDefault();
        }
    }
}
