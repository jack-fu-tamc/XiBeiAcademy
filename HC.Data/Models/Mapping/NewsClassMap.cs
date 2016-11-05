using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class NewsClassMap : EntityTypeConfiguration<NewsClass>
    {
        public NewsClassMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassID);

            // Properties
            this.Property(t => t.ClassName)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.NaviPIC)
                .HasMaxLength(150);

            this.Property(t => t.NaviContent)
                .HasMaxLength(500);
            

            // Table & Column Mappings
            this.ToTable("NewsClass");
            this.Property(t => t.ClassID).HasColumnName("ClassID");
            this.Property(t => t.ClassName).HasColumnName("ClassName");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.ClassOrder).HasColumnName("ClassOrder");
            this.Property(t => t.NaviPIC).HasColumnName("NaviPIC");
            this.Property(t => t.SmallPic).HasColumnName("SmallPic");
            this.Property(t => t.NaviContent).HasColumnName("NaviContent");
            this.Property(t => t.PageContent).HasColumnName("PageContent");
            this.Property(t => t.IsSingle).HasColumnName("IsSingle");
            this.Property(t => t.IsShowInNav).HasColumnName("IsShowInNav");
            this.Property(t => t.ShowWay).HasColumnName("ShowWay");
            this.Property(t => t.linkAddress).HasColumnName("linkAddress");
           
        }
    }
}
