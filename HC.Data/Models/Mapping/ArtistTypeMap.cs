using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class ArtistTypeMap : EntityTypeConfiguration<ArtistType>
    {
        public ArtistTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ArtistTypeID);

            // Properties
            this.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ArtistType");
            this.Property(t => t.ArtistTypeID).HasColumnName("ArtistTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.Effective).HasColumnName("Effective");
            this.Property(t => t.ArtistCategory).HasColumnName("ArtistCategory");
        }
    }
}
