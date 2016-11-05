using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class AttachmentMap : EntityTypeConfiguration<Attachment>
    {
        public AttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AttachmentID);

            // Properties
            this.Property(t => t.AttaUrl)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.AttaDescribe)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Attachment");
            this.Property(t => t.AttachmentID).HasColumnName("AttachmentID");
            this.Property(t => t.AttaType).HasColumnName("AttaType");
            this.Property(t => t.AttaUrl).HasColumnName("AttaUrl");
            this.Property(t => t.NewsID).HasColumnName("NewsID");
            this.Property(t => t.AttaOrders).HasColumnName("AttaOrders");
            this.Property(t => t.AttaDescribe).HasColumnName("AttaDescribe");
            this.Property(t => t.ClassID).HasColumnName("ClassID");
            this.Property(t => t.OriginalName).HasColumnName("OriginalName");
            // Relationships
            this.HasRequired(t => t.News)
                .WithMany(t => t.Attachments)
                .HasForeignKey(d => d.NewsID);
            this.HasOptional(t => t.NewsClass)
                .WithMany(t => t.Attachments)
                .HasForeignKey(d => d.ClassID);

        }
    }
}
