using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using ResposityOfEf;
using System.Data.SqlClient;
using HC.Data.ViewModels;

namespace HC.Service.SqlQuery
{
    public class SqlQueryService : IsqlQueryService
    {

        #region
        private IDbContext _idbcontext;
        #endregion
        public SqlQueryService(IDbContext idbcontext)
        {
            this._idbcontext = idbcontext;
        }

        public DbRawSqlQuery<int> GetNewsCount()
        {
            SqlParameter[] parameters = { };
            return this._idbcontext.SqlQuery<int>("select count(*) from News", parameters);
           // return this._idbcontext.SqlQuery<T>(sqlStr, sqlParameters);
           //SqlQuery<T>(string sqlStr, params object[] sqlParameters)
        }


        public DbRawSqlQuery<int> GetNotCheckedMessage()
        {
            SqlParameter[] parameters = { };
            return this._idbcontext.SqlQuery<int>("select count(1) from News where isDelete=2", parameters);
        }

        public DbRawSqlQuery<int> GetMonthNewsCount()
        {
            SqlParameter[] parameters = { };
            return this._idbcontext.SqlQuery<int>("select count(1) from News where DATEPART(MONTH,CreatTime)=DATEPART(month, GETDATE())", parameters);
        }

        public DbRawSqlQuery<string> GetTotalClick()
        {
            SqlParameter[] parameters = { };
            return this._idbcontext.SqlQuery<string>("select sum(ClickNum) from News", parameters);
        }

        public DbRawSqlQuery<AnalyslsNewsByMonth> getNewsAnalsysMonth(string year)
        {
            //SqlParameter[] parameters = { };
            //return this._idbcontext.SqlQuery<AnalyslsNewsByMonth>("select count(*) as total,DATEPART(MONTH,CreatTime) as monthStr from News where DATEPART(YEAR,CreatTime)="+year+"  group by DATEPART(MONTH,CreatTime)", parameters);


            
            var yearParam = new System.Data.SqlClient.SqlParameter
            {
                ParameterName = "@p_Year",
                Value = year
            };
            var results = this._idbcontext.SqlQuery<AnalyslsNewsByMonth>(
               "pro_GetNewsAnalysis @p_Year",
                yearParam
              );
            return results;
        }


        public DbRawSqlQuery<int> GetNextOrPreNewsID(int classID, int newsID, int getType)
        {
            SqlParameter[] paras=new SqlParameter[3];

            paras[0] = new SqlParameter("@p_newsID", newsID);
            paras[1] = new SqlParameter("@p_classID", classID);
            paras[2] = new SqlParameter("@p_type", getType);

            var result = this._idbcontext.SqlQuery<int>("GetNextNewsID @p_newsID, @p_classID,@p_type", paras[0], paras[1], paras[2]);
            return result;

        }

    }
}
