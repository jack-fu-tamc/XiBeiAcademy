using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HC.Data.ViewModels;
using HC.Data.Models;
using ResposityOfEf;

namespace HC.Service.EnrolSys
{
    public interface IEnrolSysService
    {
        IPagedList<StudentInfo> getStudentInfoBySearch(SerchStudentModel sModel,int pageSize,int pageIndex);
        StudentInfo GetStudentInfoById(int studentID);
    }
}
