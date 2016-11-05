using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class CxUserMap : EntityTypeConfiguration<CxUser>
    {
        public CxUserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.UserPassword)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RealName)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CxUser");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserPassword).HasColumnName("UserPassword");
            this.Property(t => t.isAdmin).HasColumnName("isAdmin");
            this.Property(t => t.RealName).HasColumnName("RealName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.UserGroupID).HasColumnName("UserGroupID");
            this.Property(t => t.Effective).HasColumnName("Effective");
            this.Property(t => t.LockTime).HasColumnName("LockTime");
            this.Property(t => t.passWordTime).HasColumnName("passWordTime");
            // Relationships
            this.HasRequired(t => t.UserGroup)
                .WithMany(t => t.CxUsers)
                .HasForeignKey(d => d.UserGroupID);

        }
    }
}
