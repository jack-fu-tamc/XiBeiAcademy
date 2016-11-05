using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.LeaveMessage
{
    public interface ILeaveMessageService
    {
        HC.Data.Models.LeaveMessage GetLeaveMessageByID(int messageID);
        void deleteLeaveMessage(HC.Data.Models.LeaveMessage entity);
        void UpdateMessage(HC.Data.Models.LeaveMessage entity);
        IPagedList<HC.Data.Models.LeaveMessage> getLeaveMessages(int pagesize, int pageIndex);
        IPagedList<HC.Data.Models.LeaveMessage> getAllLeaveMessages(int pagesize, int pageIndex); 
        void insertMessage(HC.Data.Models.LeaveMessage entity);
    }
}
