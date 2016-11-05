using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HC.Data.Models.Mapping;

namespace HC.Data.Models
{
    public partial class CXSWContext : DbContext
    {
        static CXSWContext()
        {
            Database.SetInitializer<CXSWContext>(null);
        }

        public CXSWContext()
            : base("Name=CXSWContext")
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CatDtl> CatDtls { get; set; }
        public DbSet<CxUser> CxUsers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsClass> NewsClasses { get; set; }
        public DbSet<OrderDtl> OrderDtls { get; set; }
        public DbSet<OrderMst> OrderMsts { get; set; }
        public DbSet<Pic> Pics { get; set; }
        public DbSet<Product> Products { get; set; }
     

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AttachmentMap());
            modelBuilder.Configurations.Add(new CartMap());
            modelBuilder.Configurations.Add(new CatDtlMap());
            modelBuilder.Configurations.Add(new CxUserMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new NewsClassMap());
            modelBuilder.Configurations.Add(new OrderDtlMap());
            modelBuilder.Configurations.Add(new OrderMstMap());
            modelBuilder.Configurations.Add(new PicMap());
            modelBuilder.Configurations.Add(new ProductMap());
          
        }
    }
}
