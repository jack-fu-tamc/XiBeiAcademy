using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HC.Data.Models;
using System.Data.Entity.Infrastructure;

namespace ResposityOfEf
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();

        DbRawSqlQuery<T> SqlQuery<T>(string sqlStr, params object[] sqlParameters);
    }
}
