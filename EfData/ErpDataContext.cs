using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
//using ERP.Data.Mapping.Uses;
//using ERP.Data.Domain;
using HC.Data.Models;
using HC.Data.Models.Mapping;
using System.Reflection;

namespace ResposityOfEf
{
    public class ErpDataContext : DbContext, IDbContext
    {
        public ErpDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<ErpDataContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //动态加载所有的配置
            //System.Type configType = typeof(LanguageMap);  //配置类
            System.Type configType = typeof(CxUserMap);   //
            var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();//set方法的实现交给了base去实现
        }

        public DbRawSqlQuery<T> SqlQuery<T>(string sqlStr, params object[] sqlParameters)
        {
            return this.Database.SqlQuery<T>(sqlStr, sqlParameters);
        }
    }
}
