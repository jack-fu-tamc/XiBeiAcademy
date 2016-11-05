using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class UserGroupMap : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.UserGroupID);

            // Properties
            this.Property(t => t.GroupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UserGroup");
            this.Property(t => t.UserGroupID).HasColumnName("UserGroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.ManagePermission).HasColumnName("ManagePermission");
            this.Property(t => t.SectionPermission).HasColumnName("SectionPermission");
           
        }
    }
}
