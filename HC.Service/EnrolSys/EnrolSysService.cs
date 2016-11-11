using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.Models;
using HC.Data.ViewModels;
using ResposityOfEf;


namespace HC.Service.EnrolSys
{
    public class EnrolSysService : IEnrolSysService
    {

        #region fileds
        private IRepository<Data.Models.StudentInfo> _StudentInfoRepository;
        #endregion

        #region cto
        public EnrolSysService(IRepository<HC.Data.Models.StudentInfo> StudentInfoRepository)
        {
            this._StudentInfoRepository = StudentInfoRepository;

        }
        #endregion

        public ResposityOfEf.IPagedList<Data.Models.StudentInfo> getStudentInfoBySearch(Data.ViewModels.SerchStudentModel sModel, int pageSize, int pageIndex)
        {
            var query = _StudentInfoRepository.Table;
            

            query = query.Where(x => x.RegisterTime >= sModel.sTime && x.RegisterTime <= sModel.eTime);


            if (!string.IsNullOrEmpty(sModel.StudentName))
            {
                query = query.Where(x => x.StudentName.Contains(sModel.StudentName));
            }
            if (!string.IsNullOrEmpty(sModel.StCardNo))
            {
                query = query.Where(x => x.SelfCardNo==sModel.StCardNo);
            }
            if (!string.IsNullOrEmpty(sModel.StFrom))
            {
                query = query.Where(x => x.Sfrom == sModel.StFrom);
            }

            return new PagedList<HC.Data.Models.StudentInfo>(query.OrderByDescending(x=>x.RegisterTime), pageIndex, pageSize);
        }


        public IList<StudentInfo> ExportStudentInfos(SerchStudentModel sModel)
        {
            var query = _StudentInfoRepository.Table;


            query = query.Where(x => x.RegisterTime >= sModel.sTime && x.RegisterTime <= sModel.eTime);


            if (!string.IsNullOrEmpty(sModel.StudentName))
            {
                query = query.Where(x => x.StudentName.Contains(sModel.StudentName));
            }
            if (!string.IsNullOrEmpty(sModel.StCardNo))
            {
                query = query.Where(x => x.SelfCardNo == sModel.StCardNo);
            }
            if (!string.IsNullOrEmpty(sModel.StFrom))
            {
                query = query.Where(x => x.Sfrom == sModel.StFrom);
            }

            return query.ToList();
        }


        public Data.Models.StudentInfo GetStudentInfoById(int studentID)
        {
            return _StudentInfoRepository.GetById(studentID);
        }



        public void AddStudentInfo(StudentInfo entity)
        {
            this._StudentInfoRepository.Insert(entity);
        }


        public bool isExsit(StudentInfo entity)
        {
            var query = _StudentInfoRepository.Table;
            var result = query.Where(x => x.SelfCardNo == entity.SelfCardNo && x.StudentName == entity.StudentName).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public StudentInfo getStudentInfoByCardID(string cardOrRegisterID)
        {
            var query = _StudentInfoRepository.Table;
            return query.Where(x => x.SelfCardNo == cardOrRegisterID || x.registerNo == cardOrRegisterID).FirstOrDefault();
        }
    }
}
