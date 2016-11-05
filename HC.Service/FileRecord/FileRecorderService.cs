using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResposityOfEf;

namespace HC.Service.FileRecord
{
    public class FileRecorderService : IFileRecordService
    {
        #region fileds
        private IRepository<Data.Models.FileRecord> _FileRecordRepository; 
        #endregion

        #region cto
        public FileRecorderService(IRepository<HC.Data.Models.FileRecord> fileRecorderRepository)
        {
            this._FileRecordRepository = fileRecorderRepository;
        }
        #endregion

        public IPagedList<Data.Models.FileRecord> getFileRecords(int pagesize, int pageIndex, int fileType)
        {
            //throw new NotImplementedException();
            var query = this._FileRecordRepository.Table;
            query = query.Where(x => x.FileType == fileType);
            return new PagedList<HC.Data.Models.FileRecord>(query.OrderByDescending(x => x.CreateTime), pageIndex, pagesize);
        }

        public void AddRecord(HC.Data.Models.FileRecord entity)
        {
            this._FileRecordRepository.Insert(entity);
        }

        public HC.Data.Models.FileRecord DeleteFileRecordByPath(string path)
        {
            var query = _FileRecordRepository.Table;
           var result = query.Where(x => x.Path == path).FirstOrDefault();
           _FileRecordRepository.Delete(result);
           return result;
        }
    }
}
