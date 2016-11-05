using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class LeaveMessageMap : EntityTypeConfiguration<LeaveMessage>
    {
        public LeaveMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.MessageID);

            // Properties
            this.Property(t => t.LeaveName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.QQ)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            this.Property(t => t.LeaveContent)
                .HasMaxLength(4000);

            this.Property(t => t.Answer)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("LeaveMessage");
            this.Property(t => t.MessageID).HasColumnName("MessageID");
            this.Property(t => t.LeaveName).HasColumnName("LeaveName");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.LeaveContent).HasColumnName("LeaveContent");
            this.Property(t => t.IsShow).HasColumnName("IsShow");
            this.Property(t => t.Answer).HasColumnName("Answer");
            this.Property(t => t.createTime).HasColumnName("createTime");
            this.Property(t => t.answerTime).HasColumnName("answerTime");
        }
    }
}
