using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class OpLogMap : EntityTypeConfiguration<OpLog>
    {
        public OpLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserRealName)
                .HasMaxLength(50);

            this.Property(t => t.UserAccount)
                .HasMaxLength(50);

            this.Property(t => t.OpDescriptions)
                .HasMaxLength(150);

            this.Property(t => t.OpResult)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OpLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UserRealName).HasColumnName("UserRealName");
            this.Property(t => t.UserAccount).HasColumnName("UserAccount");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.OpDescriptions).HasColumnName("OpDescriptions");
            this.Property(t => t.OpResult).HasColumnName("OpResult");
        }
    }
}
