using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResposityOfEf;

namespace HC.Service.LeaveMessage
{
    public class LeaveMessageService : ILeaveMessageService
    {
        #region fileds
        private IRepository<Data.Models.LeaveMessage> _MessageRepository;
        #endregion


        #region cto
        public LeaveMessageService(IRepository<HC.Data.Models.LeaveMessage> MessageRepository)
        {
            this._MessageRepository = MessageRepository;
        }
        #endregion
        public Data.Models.LeaveMessage GetLeaveMessageByID(int messageID)
        {

            return this._MessageRepository.GetById(messageID);
        }

        public void deleteLeaveMessage(HC.Data.Models.LeaveMessage entity)
        {
            _MessageRepository.Delete(entity);
        }

        public void UpdateMessage(Data.Models.LeaveMessage entity)
        {
            _MessageRepository.Update(entity);
        }

        public IPagedList<Data.Models.LeaveMessage> getLeaveMessages(int pagesize, int pageIndex)
        {
            var query = _MessageRepository.Table.Where(x=>x.IsShow==true);
            return new PagedList<HC.Data.Models.LeaveMessage>(query.OrderByDescending(x=>x.createTime), pageIndex, pagesize);
        }

        public IPagedList<HC.Data.Models.LeaveMessage> getAllLeaveMessages(int pagesize, int pageIndex)
        {
            var query = _MessageRepository.Table;
            return new PagedList<HC.Data.Models.LeaveMessage>(query.OrderByDescending(x => x.createTime), pageIndex, pagesize);
        }
      

        public void insertMessage(Data.Models.LeaveMessage entity)
        {
            _MessageRepository.Insert(entity);
        }
    }
}
