using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HC.Data.ViewModels;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace HC.Service.SqlQuery
{
    public interface IsqlQueryService
    {
        DbRawSqlQuery<int> GetNewsCount();//string sqlStr, params object[] sqlParameters
        DbRawSqlQuery<int> GetNotCheckedMessage();
        DbRawSqlQuery<int> GetMonthNewsCount();
        DbRawSqlQuery<int> GetTotalClick();
        DbRawSqlQuery<AnalyslsNewsByMonth> getNewsAnalsysMonth(string year);

        DbRawSqlQuery<int> GetNextOrPreNewsID(int classID, int newsID, int getType);
    }
}
