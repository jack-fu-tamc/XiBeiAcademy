using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResposityOfEf;

namespace HC.Service.FileRecord
{
    public interface IFileRecordService
    {
        IPagedList<HC.Data.Models.FileRecord> getFileRecords(int pagesize, int pageIndex,int fileType);
        void AddRecord(HC.Data.Models.FileRecord entity);
        HC.Data.Models.FileRecord DeleteFileRecordByPath(string path);
    }
}
