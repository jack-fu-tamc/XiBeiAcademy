using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class FileRecordMap : EntityTypeConfiguration<FileRecord>
    {
        public FileRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Path)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.FileType)
                .IsRequired();

            this.Property(t => t.Description)
                .IsFixedLength()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("FileRecord");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.Path).HasColumnName("Path");
            this.Property(t => t.FileType).HasColumnName("FileType");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
